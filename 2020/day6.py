total = 0
running_set = set()

infile = open("day_6_input.txt", "r")
line = infile.readline()
while line:
    if line == '\n':
        total += len(running_set)
        running_set.clear()
    else:
        for c in line.strip():
            running_set.add(c)
    line = infile.readline()

total += len(running_set)
infile.close()


print(total)

#6335
