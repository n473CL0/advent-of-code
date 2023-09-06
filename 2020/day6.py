infile = open("day_6_input.txt", "r")
line = infile.readline()
set_letters = {'a'}
total = 0
while line:
    if line == '\n':
        total += len(set_letters)
        set_letters.clear()
    else:
        for letter in line.split(' '):
            set_letters.add(letter)
print(total)
