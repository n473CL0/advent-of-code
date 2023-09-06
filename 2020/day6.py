# Part one

total = 0
running_set = set()

infile = open("day_6_input.txt", "r")
line = infile.readline()
while line:
    if line == '\n':
        total += len(running_set)
        running_set.clear()
    else:
        running_set = set(line.strip()).union(running_set)
    line = infile.readline()

total += len(running_set)
infile.close()


print(total)


# Part two
import string

total = 0
running_set = set(list(string.ascii_lowercase))

infile = open("day_6_input.txt", "r")
line = infile.readline()
while line:
    if line == '\n':
        total += len(running_set)
        running_set = set(list(string.ascii_lowercase))
    else:
        running_set.intersection_update(set(line.strip()))
    line = infile.readline()

total += len(running_set)
infile.close()


print(total)

