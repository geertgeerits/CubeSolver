﻿using System.Text.Json;

namespace CubeSolver
{
    public class Tools
    {
        // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        // Check if the cube string s represents a solvable cube.
        // 0: Cube is solvable
        // -1: There is not exactly one facelet of each colour
        // -2: Not all 12 edges exist exactly once
        // -3: Flip error: One edge has to be flipped
        // -4: Not all corners exist exactly once
        // -5: Twist error: One corner has to be twisted
        // -6: Parity error: Two corners or two edges have to be exchanged
        // 
        /// <summary>
        /// Check if the cube definition string s represents a solvable cube.
        /// </summary>
        /// <param name="s"> is the cube definition string , see <seealso cref="Facelet"/> </param>
        /// <returns> 0: Cube is solvable
        ///         -1: There is not exactly one facelet of each colour
        ///         -2: Not all 12 edges exist exactly once
        ///         -3: Flip error: One edge has to be flipped
        ///         -4: Not all 8 corners exist exactly once
        ///         -5: Twist error: One corner has to be twisted
        ///         -6: Parity error: Two corners or two edges have to be exchanged </returns>
        public static int Verify(string s)
        {
            int[] count = new int[6];
            try
            {
                for (int i = 0; i < 54; i++)
                {
                    count[(int)Enum.Parse<CubeColor>(i.ToString())]++;
                }
            }
            catch (Exception)
            {
                return -1;
            }

            for (int i = 0; i < 6; i++)
            {
                if (count[i] != 9)
                {
                    return -1;
                }
            }

            FaceCube fc = new(s);
            CubieCube cc = fc.ToCubieCube();

            return cc.Verify();
        }

        /// <summary>
        /// Generates a random cube. </summary>
        /// <returns> A random cube in the string representation. Each cube of the cube space has the same probability. </returns>
        public static string RandomCube()
        {
            CubieCube cc = new();
            Random gen = new();
            cc.SetFlip((short)gen.Next(CoordCube.N_FLIP));
            cc.SetTwist((short)gen.Next(CoordCube.N_TWIST));
            do
            {
                cc.SetURFtoDLB(gen.Next(CoordCube.N_URFtoDLB));
                cc.SetURtoBR(gen.Next(CoordCube.N_URtoBR));
            } while ((cc.EdgeParity() ^ cc.CornerParity()) != 0);
            FaceCube fc = cc.ToFaceCube();
            return fc.To_fc_String();
        }

        //public static void SerializeTable(string filename, short[,] array)
        //{
        //    EnsureFolder(Globals.cPathTables);
        //    using (Stream s = File.Open(Path.Combine(Globals.cPathTables, filename), FileMode.Create))
        //    {
        //        JsonSerializer.Serialize(s, array);
        //    }
        //}

        public static void SerializeTable(string filename, short[,] array)
        {
            EnsureFolder(Globals.cPathTables);
            using Stream s = File.Open(Path.Combine(Globals.cPathTables, filename), FileMode.Create);
            var jaggedArray = new short[array.GetLength(0)][];
            for (int i = 0; i < array.GetLength(0); i++)
            {
                jaggedArray[i] = new short[array.GetLength(1)];
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    jaggedArray[i][j] = array[i, j];
                }
            }
            JsonSerializer.Serialize(s, jaggedArray);
        }

        public static short[,] DeserializeTable(string filename)
        {
            EnsureFolder(Globals.cPathTables);
            using Stream s = File.Open(Path.Combine(Globals.cPathTables, filename), FileMode.Open);
            var result = JsonSerializer.Deserialize<short[,]>(s) ?? throw new InvalidOperationException("Deserialization returned null.");
            return result;
        }

        public static void SerializeSbyteArray(string filename, sbyte[] array)
        {
            EnsureFolder(Globals.cPathTables);
            using Stream s = File.Open(Path.Combine(Globals.cPathTables, filename), FileMode.Create);
            JsonSerializer.Serialize(s, array);
        }

        public static sbyte[] DeserializeSbyteArray(string filename)
        {
            EnsureFolder(Globals.cPathTables);
            using Stream s = File.Open(Path.Combine(Globals.cPathTables, filename), FileMode.Open);
            var result = JsonSerializer.Deserialize<sbyte[]>(s) ?? throw new InvalidOperationException("Deserialization returned null.");
            return result;
        }

        static void EnsureFolder(string path)
        {
            string? directoryName = Path.GetDirectoryName(path);
            // If path is a file name only, directory name will be null
            if (!string.IsNullOrEmpty(directoryName))
            {
                // Create all directories on the path that don't already exist
                Directory.CreateDirectory(directoryName);
            }
        }
    }
}
