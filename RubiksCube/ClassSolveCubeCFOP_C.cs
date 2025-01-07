using System.Diagnostics;
using static RubiksCube.Globals;

namespace RubiksCube
{
    internal sealed class ClassSolveCubeCFOP_C
    {
        //// Declare variables
        private const int nLoopTimesMax = 200;

        /// <summary>
        /// Solve the cube using the CFOP method
        /// </summary>
        /// <returns></returns>
        private static async Task<bool> SolveTheCubeCFOPAsync()
        {
            // Create and start a stopwatch instance
            //long startTime = Stopwatch.GetTimestamp();

            // F2L (Solving the first two layers completely)
            if (!await SolveFirstTwoLayersAsync())
            {
                return false;
            }

            // Check if the cube is solved
            if (ClassColorsCube.CheckIfSolved())
            {
                // Stop the stopwatch and get the elapsed time
                //TimeSpan delta = Stopwatch.GetElapsedTime(startTime);
                //await Application.Current!.Windows[0].Page!.DisplayAlert("SolveTheCubeCFOPAsync", $"Time elapsed (hh:mm:ss.xxxxxxx): {delta}", "OK");

                return true;
            }

            return false;
        }

        /// <summary>
        /// Solve the first two layers (F2L)
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> SolveFirstTwoLayersAsync()
        {
            string cT;
            int nLoopTimes = 0;

            while (true)
            {
                nLoopTimes++;
                if (nLoopTimes > nLoopTimesMax)
                {
                    Debug.WriteLine("CFOP_B: nLoopTimes first two layers: " + nLoopTimes);
                    return false;
                }

                // If solved, break the loop
                cT = aPieces[49];
                if (cT == aPieces[45] && cT == aPieces[46] && cT == aPieces[47] && cT == aPieces[48] && cT == aPieces[50] && cT == aPieces[51] && cT == aPieces[52] && cT == aPieces[53])
                {
                    cT = aPieces[4];
                    if (cT == aPieces[3] && cT == aPieces[5] && cT == aPieces[6] && cT == aPieces[7] && cT == aPieces[8])
                    {
                        cT = aPieces[13];
                        if (cT == aPieces[12] && cT == aPieces[14] && cT == aPieces[15] && cT == aPieces[16] && cT == aPieces[17])
                        {
                            cT = aPieces[22];
                            if (cT == aPieces[21] && cT == aPieces[23] && cT == aPieces[24] && cT == aPieces[25] && cT == aPieces[26])
                            {
                                cT = aPieces[31];
                                if (cT == aPieces[30] && cT == aPieces[32] && cT == aPieces[33] && cT == aPieces[34] && cT == aPieces[35])
                                {
                                    Debug.WriteLine("CFOP_B: number of turns first two layers: " + lCubeTurns.Count);
                                    break;
                                }
                            }
                        }
                    }
                }

                // Turn the cube
                if (nLoopTimes > 1)
                {
                    await MakeTurnAsync("y");
                }

                // When two adjacent faces (front and right) have been solved, turn the cube
                if (aPieces[49] == aPieces[46] && aPieces[49] == aPieces[47] && aPieces[49] == aPieces[50])
                {
                    if (aPieces[4] == aPieces[5] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[8])
                    {
                        if (aPieces[13] == aPieces[12] && aPieces[13] == aPieces[15] && aPieces[13] == aPieces[16])
                        {
                            await MakeTurnAsync("y");
                        }
                    }
                }

                // Bring the corner piece at the top to the right position on the top layer
                if (aPieces[49] == aPieces[11] || aPieces[49] == aPieces[18] || aPieces[49] == aPieces[38])
                {
                    if (aPieces[4] == aPieces[11] || aPieces[4] == aPieces[18] || aPieces[4] == aPieces[38])
                    {
                        if (aPieces[13] == aPieces[11] || aPieces[13] == aPieces[18] || aPieces[13] == aPieces[38])
                        {
                            await MakeTurnAsync("U");
                        }
                    }
                }

                if (aPieces[49] == aPieces[20] || aPieces[49] == aPieces[27] || aPieces[49] == aPieces[36])
                {
                    if (aPieces[4] == aPieces[20] || aPieces[4] == aPieces[27] || aPieces[4] == aPieces[36])
                    {
                        if (aPieces[13] == aPieces[20] || aPieces[13] == aPieces[27] || aPieces[13] == aPieces[36])
                        {
                            await MakeTurnAsync("U2");
                        }
                    }
                }

                if (aPieces[49] == aPieces[0] || aPieces[49] == aPieces[29] || aPieces[49] == aPieces[42])
                {
                    if (aPieces[4] == aPieces[0] || aPieces[4] == aPieces[29] || aPieces[4] == aPieces[42])
                    {
                        if (aPieces[13] == aPieces[0] || aPieces[13] == aPieces[29] || aPieces[13] == aPieces[42])
                        {
                            await MakeTurnAsync("U'");
                        }
                    }
                }

                //--------------------------------------------------------------------------------------------------------------

                // file://C:/Sources/MAUI/CubeSolver/Miscellaneous/Manuals/CubeSkills/f2l-algorithms-different-slot-positions.pdf
                // CubeSkills
                // F2L Algorithms – All Four Slot Angles - Developed by Feliks Zemdegs and Andy Klise

                // Piece 4 = 3, 6, 7 and piece 13 = 14, 16, 17
                if (aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17]) 
                {
                    // Basic Inserts (page 1)
                    // 1. U (R U' R')
                    if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[41] && aPieces[4] == aPieces[44] && aPieces[13] == aPieces[9] && aPieces[13] == aPieces[10])
                    {
                        await MakeTurnAsync("U R U' R'");
                        //continue;
                    }

                    // 2. y' (R' U' R) --- y (L' U' L)
                    if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[44] && aPieces[13] == aPieces[9] && aPieces[13] == aPieces[39])
                    {
                        await MakeTurnAsync("y' R' U' R");
                        //continue;
                    }

                    // 3. y' U' (R' U R) --- y U' (L' U L) 
                    if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[1] && aPieces[4] == aPieces[2] && aPieces[13] == aPieces[43] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("y' U' R' U R");
                        //continue;
                    }

                    // 4. (R U R')
                    if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[37] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("R U R'");
                        //continue;
                    }

                    // F2L Case 1
                    // 1. U' (R U' R' U) y' (R' U' R) --- y' U (R' U' R U') (R' U' R) 
                    if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[44] && aPieces[13] == aPieces[9] && aPieces[13] == aPieces[37])
                    {
                        await MakeTurnAsync("U' R U' R' U y' R' U' R");
                        //continue;
                    }

                    // 2. U' (R U2' R' U) y' (R' U' R) --- U' (R U2' R') d (R' U' R) 
                    if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[10] && aPieces[4] == aPieces[44] && aPieces[13] == aPieces[9] && aPieces[13] == aPieces[41])
                    {
                        await MakeTurnAsync("U' R U2 R' U y' R' U' R");
                        //continue;
                    }

                    // 3. y' U (R' U R U') (R' U' R)
                    if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[1] && aPieces[4] == aPieces[44] && aPieces[13] == aPieces[9] && aPieces[13] == aPieces[43])
                    {
                        await MakeTurnAsync("y' U R' U R U' R' U' R");
                        //continue;
                    }

                    // 4. U' (R U R' U) (R U R')
                    if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[39] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("U' R U R' U R U R'");
                        //continue;
                    }

                    // 5. R' U2' R2 U R2' U R --- y' U (R' U2 R) U' y (R U R') --- (R U' R' U) (R U' R') U2 (R U' R')
                    if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[43] && aPieces[13] == aPieces[1] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("R' U2 R2 U R2 U R");
                        //continue;
                    }

                    // 6. U' (R U' R' U) (R U R')
                    if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[41] && aPieces[13] == aPieces[10] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("U' R U' R' U R U R'");
                        //continue;
                    }

                    // F2L Case 2
                    // 1. (U' R U R') U2 (R U' R')
                    if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[37] && aPieces[4] == aPieces[44] && aPieces[13] == aPieces[9])
                    {
                        await MakeTurnAsync("U' R U R' U2 R U' R'");
                        //continue;
                    }

                    // 2. U' (R U2' R') U2 (R U' R')
                    if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[39] && aPieces[4] == aPieces[44] && aPieces[13] == aPieces[9])
                    {
                        await MakeTurnAsync("U' R U R' U2 R U' R'");
                        //continue;
                    }

                    // 3. y' (U R' U' R) U2' (R' U R) --- d (R' U' R) U2' (R' U R) --- Note: (y' U) and (d) are interchangeable
                    if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[13] == aPieces[39] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("y' U R' U' R U2 R' U R");
                        //continue;
                    }

                    // 4. y' U (R' U2 R) U2' (R' U R) --- d (R' U2 R) U2' (R' U R)
                    if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[13] == aPieces[37] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("y' U R' U2 R U2 R' U R");
                        //continue;
                    }

                    // F2L Case 3
                    // 1. U (R U2' R') U (R U' R')

                    // 2. U2 (R U R' U) (R U' R') --- (R U' R') U2 (R U R')

                    // 3. y' U' (R' U2 R) U' (R' U R)

                    // 4. y' U2 (R' U' R) U' (R' U R) --- F' L' U2 L F --- Note: The second algorithm is fewer moves, but less intuitive and less finger friendly

                    // Incorrectly Connected Pieces (page 2)

                }
            }

            return true;
        }
    }
}
