﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AntiTroll
{
    class ROMManager : IDisposable
    {
        BinaryReader reader;
        BinaryWriter writer;

        public readonly Dictionary<int, string> behavioursDescription;

        System.Windows.Forms.RichTextBox rtb;

        static int[] courseForOffset = { 0x10, 0x0F, 0x08, 0x11, 0x13, 0x0C, 0x15, 0x16,
                0x0E, 0x18, 0x19, 0x1A, 0x08, 0x1B, 0x21, 0x1C, 0x23, 0x1D, 0x12,
                0x14, 0x0D, 0x24, 0x08, 0x1E, 0x1F, 0x20, -1, 0x22, -1, -1, 0x17 };

        static string[] nameForOffset = { "C05", "C04", "Inside", "C06", "C08", "C01", "C10", "C11", "C03", "C13",
                                          "C14", "C15", "Grounds", "B1", "VC", "B2", "Aquarium", "B3", "C07",
                                          "C09", "C02", "The End", "Courty", "Slide", "MC", "WC", "B1f",
                                          "Clouds", "B2f", "B3f", "C12"  };

        static int courseBaseAddress = 0x02AC094;
        static byte objectDescriptor = 0x24;
        static int boxParamDescriptorsAddress = 0x01204000;
        static byte maxBehaviour = 0x63;

        static byte[] collectStarBehaviour = { 0x13, 0x00, 0x3E, 0x3C };
        static byte[] redCoinStarBehaviour = { 0x13, 0x00, 0x3E, 0x8C };
        static byte[] hiddenStarBehaviour = { 0x13, 0x00, 0x3E, 0xFC };

        static byte[] boxBehaviour = { 0x13, 0x00, 0x22, 0x50 };
        static byte[] checkpointBehaviour = { 0x13, 0x00, 0x3F, 0x1C };

        static byte[] rotation = { 0x00, 0x00, 0x00, 0x00, 0x00, 0xB4 };

        static byte[] secretReplaceObject = { 0xA7 }; //Splash

        public ROMManager(string fileName, System.Windows.Forms.RichTextBox rtb, string behaviourListFileName)
        {
            var a = File.Open(fileName, FileMode.Open);
            reader = new BinaryReader(a);
            writer = new BinaryWriter(a);

            behavioursDescription = BehaviourManager.Parse(behaviourListFileName);

            this.rtb = rtb;
        }
        public void Dispose()
        {
            reader.Dispose();
            writer.Dispose();
        }

        private int ReadInt32()
        {
            byte[] a32 = reader.ReadBytes(4);
            Array.Reverse(a32);
            return BitConverter.ToInt32(a32, 0);
        }

        private int ReadInt32(int offset)
        {
            reader.BaseStream.Position = offset;
            byte[] a32 = reader.ReadBytes(4);
            Array.Reverse(a32);
            return BitConverter.ToInt32(a32, 0);
        }

        private byte ReadByte()
        {
            return reader.ReadByte();
        }

        private byte ReadByte(int offset)
        {
            reader.BaseStream.Position = offset;
            return reader.ReadByte();
        }



        private byte[] ReadBehaviour(int offset)
        {
            reader.BaseStream.Position = offset + 0x14;
            return reader.ReadBytes(4);
        }

        private byte ReadBParam1(int offset)
        {
            reader.BaseStream.Position = offset + 0x10;
            return reader.ReadByte();
        }

        private byte ReadBParam2(int offset)
        {
            reader.BaseStream.Position = offset + 0x11;
            return reader.ReadByte();
        }

        private byte ReadObjectID(int offset)
        {
            reader.BaseStream.Position = offset + 0x03;
            return reader.ReadByte();
        }

        private void WriteRotation(int offset, byte[] rotation)
        {
            writer.BaseStream.Position = offset + 0x0A;
            writer.Write(rotation);
        }

        private void WriteObjectID(int offset, byte[] objectID)
        {
            reader.BaseStream.Position = offset + 0x03;
            writer.Write(objectID);
        }

        public Object[] ReadBoxBehaviours()
        {
            Object[] ret = new Object[maxBehaviour];
            reader.BaseStream.Position = boxParamDescriptorsAddress;
            while (true)
            {
                byte[] data = reader.ReadBytes(8);
                if (data[0] == maxBehaviour) return ret;
                Object obj = new Object(data[1], data[2], data[3], IPAddress.HostToNetworkOrder(BitConverter.ToInt32(data, 4)));
                ret[data[0]] = obj;
            }

        }

        public void DetrollLevels(ISet<int> trollObjects, bool isRevertingStars, bool isShowingSecrets)
        {
            for (int index = 0; index < courseForOffset.Length; index++)
            {
                int levelAddressStart = ReadInt32(courseBaseAddress + index * 0x14 + 0x04); //base offset + offset for descriptor + address in descriptor
                int levelAddressEnd = ReadInt32();
                
                rtb.AppendText(String.Format("{0} is address {1:x} -> {2:x} - {3:x}\n", nameForOffset[index], reader.BaseStream.Position - 0x08, levelAddressStart, levelAddressEnd));
                DetrollData(levelAddressStart, levelAddressEnd, trollObjects, isRevertingStars, isShowingSecrets);
            }
        }

        private void Detroll(int offset)
        {
            WriteRotation(offset, rotation);
        }

        private void DetrollData(int start, int end, ISet<int> trollObjects, bool isRevertingStars, bool isShowingSecrets)
        {
            for (int offset = start; offset < end; offset++)
            {
                reader.BaseStream.Position = offset;
                if (reader.ReadByte() != objectDescriptor) continue; //work with 3D object only
                byte[] behaviour = ReadBehaviour(offset);
                if (behaviour.SequenceEqual(collectStarBehaviour) ||
                    behaviour.SequenceEqual(redCoinStarBehaviour) ||
                    behaviour.SequenceEqual(hiddenStarBehaviour))
                {
                    byte starByte = ReadBParam1(offset);
                    if (isRevertingStars && starByte > 0x06)
                    {
                        rtb.AppendText(String.Format("[TrollStar] '{0:x}' Star {1} detected!\n", offset, starByte));
                        Detroll(offset);
                    }
                }

                if (behaviour.SequenceEqual(boxBehaviour))
                {
                    byte starByte = ReadBParam2(offset);

                    if (trollObjects.Contains(starByte))  //not a star
                    {
                        rtb.AppendText(String.Format("[Weird Box] '{0:x}' WObj {1} detected!\n", offset, starByte));
                        Detroll(offset);
                    }
                    if (starByte == 0x8) //star for random shit
                    {
                        byte starByteParam = ReadBParam1(offset);
                        if (isRevertingStars && starByteParam > 0x06)
                        {
                            rtb.AppendText(String.Format("[Troll Box] '{0:x}' Star {1} detected!\n", offset, starByte));
                            Detroll(offset);
                        }
                    }
                }

                if (behaviour.SequenceEqual(checkpointBehaviour))
                {
                    byte objectID = ReadObjectID(offset);
                    if (isShowingSecrets && objectID == 0)
                    {
                        rtb.AppendText(String.Format("[InvSecret] '{0:x}' Invisible secret detected!\n", offset));
                        WriteObjectID(offset, secretReplaceObject);
                    }
                }
            }

        }

    }
}
