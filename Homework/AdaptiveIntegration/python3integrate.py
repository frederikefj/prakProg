from scipy.integrate import quad
import numpy as np

def f2(x):
    return 1/np.sqrt(x)

def f4(x):
    return np.log(x)/np.sqrt(x)

y, e, info = quad(f4, 0, 1, full_output=1, epsabs=1e-3, epsrel=1e-3)

print(y, e)
print("")
print(info["neval"])