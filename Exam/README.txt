
Exam project 04: Yet another stochastic global optimizer
_________________________________________________________________________________
Implement a stochastic global optimizer using the following algorithm,

1. Use the given number of seconds to search for the global minimum
   of the given function in the given volume by sampling the function
   using a low-discrepancy sequence

2. From the best point found at the previous step run your
   quasi-newton minimizer
_________________________________________________________________________________

I have decided to interpret the level of my exam project as:

A. I implemented the above-mentioned global optimizer and tested it on a test-
   function with many local minima but only one global minima

B. To deal with functions that doesn't have nice second derivatives i implemented
   an option to use the downhill-simplex algorithm for local minimization, 
   (instead of the quasi-newton minimizer.)

   I then used the global minimizer to find the largest unsampled circle in a 
   sample of randomly distributed 2d points. (I minimized minus the distance from 
   other points or the edge of the [0,1]^2 area.)	
__________________________________________________________________________________

The global optimizer is in "optimization.cs", some other used functions are in
"func.cs" and the testing of the optimizer is in "main.cs" with results shown in
"Out.txt" and the related plot "points.svg".




