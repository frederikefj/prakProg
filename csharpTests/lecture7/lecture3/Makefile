TIME = time --portability --append --output $@

N = 2e9
Out.txt : main.exe
	>$@
	$(TIME) mono main.exe -terms:$N -threads:1 >> $@
	$(TIME) mono main.exe -terms:$N -threads:8 >> $@
	$(TIME) mono main.exe -terms:$N -threads:16 >> $@
	$(TIME) mono main.exe -terms:$N -threads:40 >> $@

main.exe : main.cs
	mcs main.cs -target:exe -out:main.exe


