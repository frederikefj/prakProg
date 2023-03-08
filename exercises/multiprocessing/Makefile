TIME = time --portability --append --output $@

N = 2e9
Out.txt : main.exe pfor.exe
	>$@
	$(TIME) mono main.exe -terms:$N -threads:1 >> $@
	$(TIME) mono main.exe -terms:$N -threads:8 >> $@
	$(TIME) mono main.exe -terms:$N -threads:16 >> $@
	$(TIME) mono main.exe -terms:$N -threads:40 >> $@
	$(TIME) mono pfor.exe -terms:$N -threads:1 >> $@

main.exe : main.cs
	mcs main.cs -target:exe -out:main.exe

pfor.exe : pfor.cs
	mcs $<

.PHONEY:clean
clean:
	$(RM) -rf *.txt *.exe 
