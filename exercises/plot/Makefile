Default : Out.txt error.svg gamma.svg lngamma.svg

Out.txt out.erf.data out.erfTab.data out.gammaTab.data out.lngamma.data out.lngammaTab.data out.gamma.data : main.exe
	mono main.exe > Out.txt -target:$@

main.exe : main.cs
	mcs main.cs -target:exe -out:main.exe

error.svg : error.gpi out.erf.data out.erfTab.data
	gnuplot error.gpi

gamma.svg : gamma.gpi out.gamma.data out.gammaTab.data
	gnuplot gamma.gpi

lngamma.svg : lngamma.gpi out.lngamma.data out.lngammaTab.data
	gnuplot lngamma.gpi

clean :
	$(RM) *.dll *.exe [Oo]ut* *.svg

