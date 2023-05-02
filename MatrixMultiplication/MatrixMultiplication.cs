using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;


namespace Problem
{
    // *****************************************
    // DON'T CHANGE CLASS OR FUNCTION NAME
    // YOU CAN ADD FUNCTIONS IF YOU NEED TO
    // *****************************************
    public static class MatrixMultiplication
    {
        #region YOUR CODE IS HERE

        //Your Code is Here:
        //==================
        /// <summary>
        /// Multiply 2 square matrices in an efficient way [Strassen's Method]
        /// </summary>
        /// <param name="M1">First square matrix</param>
        /// <param name="M2">Second square matrix</param>
        /// <param name="N">Dimension (power of 2)</param>
        /// <returns>Resulting square matrix</returns>




        static int[][,] MatrixDiv(int[,] M, int N)
        {
            int[][,] Res = new int[4][,];

            if (M.GetLength(0) > 0 && M.GetLength(1) > 0 && N == 1)
            {
                for (int k = 0; k < 4; k++)
                {
                    Res[k] = new int[1, 1];
                }
                Res[0][0, 0] = M[0, 0];
                Res[1][0, 0] = M[0, 1];
                Res[2][0, 0] = M[1, 0];
                Res[3][0, 0] = M[1, 1];
            }

            else
            {
                for (int k = 0; k < 4; k++)
                {
                    Res[k] = new int[N / 2, N / 2];
                }

                for (int i = 0; i < N / 2; i++)
                {
                    for (int j = 0; j < N / 2; j++)
                    {
                        Res[0][i, j] = M[i, j];
                        Res[1][i, j] = M[i, j + (N / 2)];
                        Res[2][i, j] = M[i + (N / 2), j];
                        Res[3][i, j] = M[i + (N / 2), j + (N / 2)];

                    }
                }
            }


            #region Print Each Matrix


            //Console.WriteLine("\nUL Matrix\n");

            //p.PrintMat(Res[0]);

            //Console.WriteLine("\nUR Matrix\n");

            //p.PrintMat(Res[1]);

            //Console.WriteLine("\nDL Matrix\n");

            //p.PrintMat(Res[2]);

            //Console.WriteLine("\nDR Matrix\n");

            //p.PrintMat(Res[3]);

            #endregion


            return Res;
        }

        static int[,] MatrixCombine(int[,] D1, int[,] D2, int N, int S)
        {
            int[,] Res = new int[N, N];

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (S == 0)
                        Res[i, j] = D1[i, j] + D2[i, j];
                    else
                        Res[i, j] = D1[i, j] - D2[i, j];
                }
            }
            return Res;
        }

        static int[,] Naive(int[,] M1, int[,] M2, int N)
        {
            int[,] Result = new int[N, N];


            for (int i = 0; i < N; i++)
            {
                for (int k = 0; k < N; k++)
                {
                    for (int j = 0; j < N; j++)
                    {
                        Result[i, j] += M1[i, k] * M2[k, j];
                    }
                }
            }
            return Result;
        }

        #region unUSED Algorithms

        static void PrintMat(int[,] M)
        {
            for (int i = 0; i < M.GetLength(0); i++)
            {
                for (int j = 0; j < M.GetLength(0); j++)
                {
                    Console.Write(M[i, j] + " ");

                }
                Console.WriteLine();
            }
        }

        static int[,] Old_Naive(int[,] M1, int[,] M2, int N)
        {
            int[,] Result = new int[N, N];


            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    for (int k = 0; k < N; k++)
                    {
                        Result[i, j] += M1[i, k] * M2[k, j];
                    }
                }
            }
            return Result;
        }

        static int[,] D_C(int[,] M1, int[,] M2, int N)
        {


            int[,] Result = new int[N, N];
            int[][,] DivMat1 = new int[4][,];
            int[][,] DivMat2 = new int[4][,];

            int[,] AX = new int[N, N];
            int[,] BZ = new int[N, N];
            int[,] CX = new int[N, N];
            int[,] DZ = new int[N, N];

            int[,] AY = new int[N, N];
            int[,] BL = new int[N, N];
            int[,] CY = new int[N, N];
            int[,] DL = new int[N, N];

            int[,] A = new int[N, N];
            int[,] B = new int[N, N];
            int[,] C = new int[N, N];
            int[,] D = new int[N, N];






            if (M1.GetLength(0) > 0 && M1.GetLength(1) > 0 && M2.GetLength(0) > 0 && M2.GetLength(1) > 0 && N == 1)
            {
                Result[0, 0] = M1[0, 0] * M2[0, 0];
            }
            else
            {
                DivMat1 = MatrixDiv(M1, N);
                DivMat2 = MatrixDiv(M2, N);


                AX = Naive(DivMat1[0], DivMat2[0], N / 2);
                BZ = Naive(DivMat1[1], DivMat2[2], N / 2);
                CX = Naive(DivMat1[2], DivMat2[0], N / 2);
                DZ = Naive(DivMat1[3], DivMat2[2], N / 2);

                AY = Naive(DivMat1[0], DivMat2[1], N / 2);
                BL = Naive(DivMat1[1], DivMat2[3], N / 2);
                CY = Naive(DivMat1[2], DivMat2[1], N / 2);
                DL = Naive(DivMat1[3], DivMat2[3], N / 2);

                A = MatrixCombine(AX, BZ, N / 2, 0);
                B = MatrixCombine(AY, BL, N / 2, 0);
                C = MatrixCombine(CX, DZ, N / 2, 0);
                D = MatrixCombine(CY, DL, N / 2, 0);


                #region Print each Quarter
                //Console.WriteLine("\nMatrix A = \n");
                //PrintMat(A);
                //Console.WriteLine("\nMatrix B = \n");
                //PrintMat(B);
                //Console.WriteLine("\nMatrix C = \n");
                //PrintMat(C);
                //Console.WriteLine("\nMatrix D = \n");
                //PrintMat(D);
                //Console.WriteLine();
                #endregion


                for (int i = 0; i < N / 2; i++)
                {
                    for (int j = 0; j < N / 2; j++)
                    {
                        Result[i, j] = A[i, j];
                        Result[i, j + (N / 2)] = B[i, j];
                        Result[i + (N / 2), j] = C[i, j];
                        Result[i + (N / 2), j + (N / 2)] = D[i, j];
                    }


                }

            }


            return Result;

        }
        #endregion

        static int[,] Strassen(int[,] M1, int[,] M2, int N)
        {

            #region INITIALIZATION
            int[,] Result = new int[N, N];
            int[][,] DivMat1 = new int[4][,];
            int[][,] DivMat2 = new int[4][,];

            int[,] P1 = new int[N, N];
            int[,] P2 = new int[N, N];
            int[,] P3 = new int[N, N];
            int[,] P4 = new int[N, N];

            int[,] P5 = new int[N, N];
            int[,] P6 = new int[N, N];
            int[,] P7 = new int[N, N];
            int[,] P8 = new int[N, N];

            int[,] A = new int[N, N];
            int[,] B = new int[N, N];
            int[,] C = new int[N, N];
            int[,] D = new int[N, N];

            int[,] tmpA = new int[N, N];
            int[,] tmpD = new int[N, N];
            #endregion

            //Base Case
            if (N <= 512)
            {
                Result = Naive(M1, M2, N);
            }

            else
            {
                //DivMat1 = MatrixDiv(M1, N);
                //DivMat2 = MatrixDiv(M2, N);

                #region Divsion With Tasks
                Task[] DivTask = new Task[2];
                DivTask[0] = Task.Run(() =>
                {
                    DivMat1 = MatrixDiv(M1, N);
                });
                DivTask[1] = Task.Run(() =>
                {
                    DivMat2 = MatrixDiv(M2, N);
                });

                for (int i = 0; i < 2; i++)
                {
                    DivTask[i].Wait();
                }
                #endregion

                #region Strassen with Tasks
                Task[] tasks = new Task[7];
                tasks[0] = Task.Run(() => { P1 = Strassen(DivMat1[0], MatrixCombine(DivMat2[1], DivMat2[3], N / 2, 1), N / 2); });

                tasks[1] = Task.Run(() => { P2 = Strassen(MatrixCombine(DivMat1[0], DivMat1[1], N / 2, 0), DivMat2[3], N / 2); });

                tasks[2] = Task.Run(() => { P3 = Strassen(MatrixCombine(DivMat1[2], DivMat1[3], N / 2, 0), DivMat2[0], N / 2); });

                tasks[3] = Task.Run(() => { P4 = Strassen(DivMat1[3], MatrixCombine(DivMat2[2], DivMat2[0], N / 2, 1), N / 2); });

                tasks[4] = Task.Run(() => { P5 = Strassen(MatrixCombine(DivMat1[0], DivMat1[3], N / 2, 0), MatrixCombine(DivMat2[0], DivMat2[3], N / 2, 0), N / 2); });
                tasks[5] = Task.Run(() => { P6 = Strassen(MatrixCombine(DivMat1[1], DivMat1[3], N / 2, 1), MatrixCombine(DivMat2[2], DivMat2[3], N / 2, 0), N / 2); });
                tasks[6] = Task.Run(() => { P7 = Strassen(MatrixCombine(DivMat1[0], DivMat1[2], N / 2, 1), MatrixCombine(DivMat2[0], DivMat2[1], N / 2, 0), N / 2); });


                for (int i = 0; i < 7; i++)
                {
                    tasks[i].Wait();
                }
                #endregion

                #region Combination with Tasks
                Task[] T = new Task[2];
                T[0] = Task.Run(() => { tmpA = MatrixCombine(P4, P2, N / 2, 1); });
                T[1] = Task.Run(() => { tmpD = MatrixCombine(MatrixCombine(P5, P3, N / 2, 1), P7, N / 2, 1); });

                for (int i = 0; i < 2; i++)
                {
                    T[i].Wait();
                }


                Task[] taskC = new Task[4];
                taskC[0] = Task.Run(() => { A = MatrixCombine(MatrixCombine(P5, tmpA, N / 2, 0), P6, N / 2, 0); });
                taskC[1] = Task.Run(() =>
                {
                    B = MatrixCombine(P1, P2, N / 2, 0);
                });
                taskC[2] = Task.Run(() =>
                {
                    C = MatrixCombine(P3, P4, N / 2, 0);
                });
                taskC[3] = Task.Run(() =>
                {
                    D = MatrixCombine(P1, tmpD, N / 2, 0);
                });


                for (int i = 0; i < 4; i++)
                {
                    taskC[i].Wait();
                }
                #endregion





                


                for (int i = 0; i < N / 2; i++)
                {
                    for (int j = 0; j < N / 2; j++)
                    {
                        Result[i, j] = A[i, j];
                        Result[i, j + (N / 2)] = B[i, j];
                        Result[i + (N / 2), j] = C[i, j];
                        Result[i + (N / 2), j + (N / 2)] = D[i, j];
                    }
                }

            }


            return Result;
        }


        static public int[,] MatrixMultiply(int[,] M1, int[,] M2, int N)
        {
            int[,] Res = new int[N, N];

            Res = Strassen(M1, M2, N);


            #region Generate 2 Matrices M1, M2 (N*N)

            //M1 = new int[N, N];
            //M2 = new int[N, N];

            //int c = 1;
            //int F = 17;
            //Console.WriteLine("\n Mat 1\n");
            //for (int i = 0; i < 4; i++)
            //{
            //    for (int j = 0; j < 4; j++)
            //    {
            //        M1[i, j] = c;
            //        c++;
            //        Console.Write(M1[i, j] + " ");

            //    }
            //    Console.WriteLine();
            //}
            //Console.WriteLine("\n Mat 2\n");

            //for (int i = 0; i < 4; i++)
            //{
            //    for (int j = 0; j < 4; j++)
            //    {
            //        M2[i, j] = F;
            //        F++;
            //        Console.Write(M2[i, j] + " ");

            //    }
            //    Console.WriteLine();
            //}
            #endregion


            #region Diagnostics
            //Stopwatch watch = new Stopwatch();
            //watch.Start();



            ////Res = Old_Naive(M1, M2, N);

            //watch.Stop();
            //Console.WriteLine("\n\nStrassen Time = " + watch.Elapsed + "\n\n");


            //Stopwatch watch2 = new Stopwatch();
            //watch2.Start();
            //Res = Naive(M1, M2, N);

            //watch2.Stop();
            //Console.WriteLine("\n\nNaive Time = " + watch2.Elapsed + "\n\n");

            //Stopwatch watch3 = new Stopwatch();
            //watch3.Start();
            //Res = Old_Naive(M1, M2, N);

            //watch3.Stop();
            //Console.WriteLine("\n\nOld_Naive Time = " + watch3.Elapsed + "\n\n");



            //Res = Old_Naive(M1, M2, N);

            //watch2.Stop();
            //Console.WriteLine("\n\nStrassen Time = " + watch2.Elapsed + "\n\n");
            #endregion


            return Res;
        }

        #endregion
    }
}
