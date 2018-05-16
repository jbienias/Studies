def difference():
	array = load_file()
	for i in range(0, len(array), 2):
		bin1 = bin(int(array[i], 16))[2:].zfill((len(array[i])-1) * 4)
		bin2 = bin(int(array[i+1], 16))[2:].zfill((len(array[i+1])-1) * 4)
		dlugosc = len(bin1)
		roznica = ([x == y for (x, y) in zip(bin1, bin2)].count(False))
		procent = (float(roznica) / float(dlugosc)) * 100
		print("Difference in bites: %d (from %d, percentage: %2.f%%)." % (roznica, dlugosc, procent))


def load_file():
	with open("hash.txt", "r") as ins:
		array = []
		for line in ins:
			array.append(line)
	return array


difference()