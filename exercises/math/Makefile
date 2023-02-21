Out.txt : main.exe
	mono main.exe > Out.txt

sfuns.dll : gamma.cs
	mcs $^ -target:library -out:sfuns.dll

main.exe : main.cs sfuns.dll
	mcs main.cs -reference:sfuns.dll -target:exe -out:main.exe
