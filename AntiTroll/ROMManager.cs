using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntiTroll
{
    class ROMManager : IDisposable
    {
        BinaryReader reader;
        BinaryWriter writer;

        System.Windows.Forms.RichTextBox rtb;

        static int[] courseForOffset = { 0x10, 0x0F, 0x08, 0x11, 0x13, 0x0C, 0x15, 0x16,
                0x0E, 0x18, 0x19, 0x1A, 0x08, 0x1B, 0x21, 0x1C, 0x23, 0x1D, 0x12,
                0x14, 0x0D, 0x24, 0x08, 0x1E, 0x1F, 0x20, -1, 0x22, -1, -1, 0x17 };

        static int courseBaseAddress = 0x02AC094;
        static byte objectDescriptor = 0x24;

        static byte[] collectStarBehaviour = { 0x00, 0x3E, 0x3C };
        static byte[] redCoinStarBehaviour = { 0x00, 0x3E, 0x8C };
        static byte[] hiddenStarBehaviour = { 0x00, 0x3E, 0xFC };

        static byte[] boxBehaviour = { 0x00, 0x22, 0x50 };

        static byte[] bossWhompBehaviour = { 0x00, 0x2B, 0xB8 };
        static byte[] bossBobombBehaviour = { 0x00, 0x01, 0xF4 };
        static byte[] bossKoopaBehaviour = { 0x00, 0x45, 0x80 };
        static byte[] bossBullyBehaviour = { 0x00, 0x36, 0x60 };
        static byte[] bossBalconyBehaviour = { 0x00, 0x27, 0x68 };
        static byte[] bossPenguinBehaviour = { 0x00, 0x20, 0x88 };
        static byte[] bossBooBuddyBehaviour = { 0x00, 0x27, 0x90 };
        static byte[] bossWigglerBehaviour = { 0x00, 0x48, 0x98 };
        static byte[] bossBlizzardBehaviour = { 0x00, 0x4D, 0xBC };
        static byte[] bossPiranhaBehaviour = { 0x00, 0x51, 0x20 };

        static byte[] bossMIPSBehaviour = { 0x00, 0x44, 0xFC };

        static byte[] rotation = { 0x00, 0x00, 0x00, 0x00, 0x00, 0xB4 };

        public ROMManager(string fileName, System.Windows.Forms.RichTextBox rtb)
        {
            var a = File.Open(fileName, FileMode.Open);
            reader = new BinaryReader(a);
            writer = new BinaryWriter(a);

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
            reader.BaseStream.Position = offset + 0x15;
            return reader.ReadBytes(3);
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

        private void WriteRotation(int offset, byte[] rotation)
        {
            writer.BaseStream.Position = offset + 0x0A;
            writer.Write(rotation);
        }

        public void TraverseLevels()
        {
            for (int index = 0; index < courseForOffset.Length; index++)
            {
                int levelAddressStart = ReadInt32(courseBaseAddress + index * 0x14 + 0x04); //base offset + offset for descriptor + address in descriptor
                int levelAddressEnd = ReadInt32();
                
                rtb.AppendText(String.Format("{0} is address {1:x} -> {2:x} - {3:x}\n", index, reader.BaseStream.Position - 0x08, levelAddressStart, levelAddressEnd));
                TraverseData(levelAddressStart, levelAddressEnd);
            }
        }

        private void Detroll(int offset)
        {
            WriteRotation(offset, rotation);
        }

        private void TraverseData(int start, int end)
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
                    if (starByte > 0x06)
                    {
                        rtb.AppendText(String.Format("[TrollStar] '{0:x}' Star {1} detected!\n", offset, starByte));
                        Detroll(offset);
                    }
                }

                if (behaviour.SequenceEqual(boxBehaviour))
                {
                    byte starByte = ReadBParam2(offset);

                    if (starByte > 0xF)  //not a star
                    {
                        rtb.AppendText(String.Format("[Weird Box] '{0:x}' WObj {1} detected!\n", offset, starByte));
                        Detroll(offset);
                    }
                    if (starByte == 0x8) //star for random shit
                    {
                        byte starByteParam = ReadBParam1(offset);
                        if (starByteParam > 0x06)
                        {
                            rtb.AppendText(String.Format("[Troll Box] '{0:x}' Star {1} detected!\n", offset, starByte));
                            Detroll(offset);
                        }
                    }
                }
            }

        }

    }
}
