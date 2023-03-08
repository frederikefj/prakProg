Out.txt : main.exe
	mono main.exe > Out.txt

main.exe : main.cs matrix.dll QRGS.dll
	mcs -reference:matrix.dll -reference:QRGS.dll main.cs -target:exe -out:main.exe

DIR = $(HOME)/repos/prakProg/matlib/matrix

DIR = ../../matlib/matrix

matrix.dll : $(DIR)/matrix.cs $(DIR)/vector.cs
	mcs -target:library -out:./matrix.dll $^

QRGS.dll : QRGS.cs matrix.dll
	mcs -target:library -reference:matrix.dll -out:./QRGS.dll QRGS.cs

clean : 
	rm -f main.exe matrix.dll Out.dll
