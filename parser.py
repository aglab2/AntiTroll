with open("behav.txt") as f, open("out.txt", "w") as o:
	while(True):
		line1 = f.readline()
		if (line1 == ""):
			break
		if line1.startswith("ROM Addr"):
			addr = line1[-9:-1]
			o.write(addr)
			o.write(" : ")
			line2 = f.readline()
			if (line2.startswith(">")):
				o.write("Unused behaviour\n")
			else:
				o.write(line2[13:])