using System;
using static System.Console;
using static System.Math;


public class QRGS{	
	// Stabilized gramsmith QR decomposition
	public static void decomb(matrix A, matrix R){ 
		int m = A.size2;
		for (int i = 0; i<m; i++) {
			R[i,i] = A[i].norm();
			A[i] /= R[i,i];
			for (int j=i+1; j<m; j++)	{
				R[i,j] = A[i].dot(A[j]);
				A[j] -= A[i]*R[i,j];
				}
			}
			
		}

	// solve an uppertriangular matrix
	public static vector solveR(matrix R, vector b) {
		int n = R.size1;
		var s = new vector(n);
		for (int i = 0; i<n; i++) {
			s[n-1-i] = b[n-1-i]/R[n-1-i, n-1-i];
			for (int j=n-i; j<n; j++) {
				s[n-1-i] -= R[j][n-1-i]*s[j]/R[n-1-i, n-1-i];
				}
			}
		return s;
		}

	// solve a generel tall matrix (solutions only produve Ax=b for a square matrix)
	public static vector solve(matrix Q, matrix R, vector b) {
		var QTb = Q.transpose()*b;
		var s = QRGS.solveR(R, QTb);
		return s;
		}

	public static double det(matrix R) {
		double detR = 1;		
		int n = R.size1;
		for (int i=0; i<n; i++) {
			detR *= R[i,i];
			}
		return detR;
		}
	
	public static matrix inverse(matrix Q, matrix R) {
		int m = Q.size2;
		var inR = new matrix(m, m);
		for (int i=0; i<m; i++) {
			vector i_unit = new vector(m);
			i_unit[i] = 1;
			vector i_col = QRGS.solveR(R, i_unit);
			inR[i] += i_col;	
			}
		var B = inR*Q.transpose();
		return B;
		}
}
