
using System;
using System.Dynamic;
using System.Drawing;
using System.Collections.Generic;

namespace SaY_DeF.Source
{
    static class FieldGeneration
    {
        static TyleType[,] field;
        static Random r;
        static int indSp, indFn, roadType;
        public enum TyleType
        {
            Road,
            Border,
            Start,
            Finish,
            UsualTyle,
            BigTyle,
            Empty,
            HelpingRoad
        }


        static public TyleType[,] GenerateField()
        {
            r = new Random();
            field = new TyleType[11, 26];
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    field[i, j] = TyleType.Empty;
                }

            }
            SetBaseAndSpawn();
            SetBorders();
            SetRoad();
            SetTyles();
            CorrectTyles();
            CorrectRoad();
            return field;
        }


        static void SetBorders()
        {
            for (int i = 0; i < field.GetLength(0); i++)
            {
                if (field[i, 0] != TyleType.Finish)
                    field[i, 0] = TyleType.Border;
                if (field[i, 25] != TyleType.Start)
                    field[i, 25] = TyleType.Border;
            }
            if (indSp == 4)
            {
                roadType = 1;
                field[0, 1] = TyleType.Border;
                field[0, 2] = TyleType.Border;
                field[0, 3] = TyleType.Border;
                field[1, 1] = TyleType.Border;
                field[1, 2] = TyleType.Border;
                field[1, 3] = TyleType.Border;
                field[0, 4] = TyleType.Border;
                field[2, 1] = TyleType.Border;
                if (r.Next(0, 10) < 5)
                {
                    field[1, 4] = TyleType.Border;
                    field[0, 5] = TyleType.Border;
                }
                field[10, 1] = TyleType.Border;
                field[10, 2] = TyleType.Border;
                field[9, 1] = TyleType.Border;

                field[7, 1] = TyleType.Border;
                field[8, 1] = TyleType.Border;
                if (r.Next(0, 10) < 5)
                {
                    field[6, 1] = TyleType.Border;
                    field[7, 2] = TyleType.Border;
                    field[8, 2] = TyleType.Border;
                    field[8, 3] = TyleType.Border;
                    field[9, 2] = TyleType.Border;
                    field[9, 3] = TyleType.Border;
                    field[9, 4] = TyleType.Border;
                    field[10, 3] = TyleType.Border;
                    field[10, 4] = TyleType.Border;
                    field[10, 5] = TyleType.Border;
                    field[9, 5] = TyleType.Border;
                    roadType = -1;
                }
                if (r.Next(0, 10) < 5)
                {
                    field[10, 6] = TyleType.Border;
                    field[10, 7] = TyleType.Border;
                }
            }
            else if (indSp == 5)
            {
                field[0, 1] = TyleType.Border;
                field[1, 1] = TyleType.Border;
                field[2, 1] = TyleType.Border;
                field[3, 1] = TyleType.Border;
                field[0, 2] = TyleType.Border;

                if (r.Next(0, 10) < 5)
                {
                    field[0, 3] = TyleType.Border;
                    field[0, 4] = TyleType.Border;
                    field[1, 2] = TyleType.Border;
                    if (r.Next(0, 10) < 6)
                    {
                        field[1, 3] = TyleType.Border;
                        field[0, 5] = TyleType.Border;
                    }

                }

                field[10, 1] = TyleType.Border;
                field[10, 2] = TyleType.Border;
                field[9, 1] = TyleType.Border;
                field[9, 2] = TyleType.Border;
                field[8, 1] = TyleType.Border;
                if (r.Next(0, 10) < 6)
                {
                    field[7, 5] = TyleType.Border;
                    field[7, 6] = TyleType.Border;

                    field[8, 4] = TyleType.Border;
                    field[8, 5] = TyleType.Border;
                    field[8, 6] = TyleType.Border;
                    field[8, 7] = TyleType.Border;

                    field[9, 3] = TyleType.Border;
                    field[9, 4] = TyleType.Border;
                    field[9, 5] = TyleType.Border;
                    field[9, 6] = TyleType.Border;
                    field[9, 7] = TyleType.Border;

                    field[10, 3] = TyleType.Border;
                    field[10, 4] = TyleType.Border;
                    field[10, 5] = TyleType.Border;
                    field[10, 6] = TyleType.Border;
                    field[10, 7] = TyleType.Border;
                    field[10, 8] = TyleType.Border;

                    roadType = -1;
                }
                else
                    roadType = 1;
            }
            else
            {
                roadType = 1;
                field[8, 1] = TyleType.Border;
                field[9, 1] = TyleType.Border;
                field[9, 2] = TyleType.Border;
                field[9, 3] = TyleType.Border;
                field[9, 4] = TyleType.Border;
                field[10, 1] = TyleType.Border;
                field[10, 2] = TyleType.Border;
                field[10, 3] = TyleType.Border;
                field[10, 4] = TyleType.Border;
                field[10, 5] = TyleType.Border;
                field[10, 6] = TyleType.Border;
                if (r.Next(0, 10) < 5)
                {
                    field[10, 7] = TyleType.Border;
                    field[8, 2] = TyleType.Border;
                    field[8, 3] = TyleType.Border;
                    roadType = -1;
                }
                field[0, 1] = TyleType.Border;
                field[1, 1] = TyleType.Border;
                field[2, 1] = TyleType.Border;
                field[0, 2] = TyleType.Border;
                field[1, 2] = TyleType.Border;
                if (r.Next(0, 10) < 6 && roadType == -1)
                {
                    field[3, 1] = TyleType.Border;
                    field[0, 3] = TyleType.Border;
                    field[0, 4] = TyleType.Border;
                }
            }
            if (indFn == 4)
            {
                field[0, 24] = TyleType.Border;
                field[1, 24] = TyleType.Border;
                field[2, 24] = TyleType.Border;
                field[0, 23] = TyleType.Border;
                if (r.Next(0, 10) < 5)
                    field[2, 24] = TyleType.Empty;
                if (r.Next(0, 10) < 5)
                    field[0, 22] = TyleType.Border;
                field[10, 24] = TyleType.Border;
                field[9, 24] = TyleType.Border;
                field[8, 24] = TyleType.Border;
                field[9, 23] = TyleType.Border;
                field[10, 23] = TyleType.Border;
                if (r.Next(0, 10) > 5)
                    field[7, 24] = TyleType.Border;
                else
                {
                    field[10, 22] = TyleType.Border;
                    field[9, 22] = TyleType.Border;
                    field[10, 21] = TyleType.Border;
                }
            }
            else if (indFn == 5)
            {
                field[0, 24] = TyleType.Border;
                field[1, 24] = TyleType.Border;
                field[2, 24] = TyleType.Border;
                field[0, 23] = TyleType.Border;
                if (r.Next(0, 10) < 5)
                    field[2, 24] = TyleType.Empty;
                else
                    field[3, 24] = TyleType.Border;
                if (r.Next(0, 10) < 5)
                    field[0, 22] = TyleType.Border;
                if (r.Next(0, 10) < 5)
                    field[1, 23] = TyleType.Border;

                field[10, 24] = TyleType.Border;
                field[9, 24] = TyleType.Border;
                field[8, 24] = TyleType.Border;
                field[9, 23] = TyleType.Border;
                field[10, 23] = TyleType.Border;
                if (r.Next(0, 10) > 5)
                    field[7, 24] = TyleType.Border;
                else
                {
                    field[10, 22] = TyleType.Border;
                    field[9, 22] = TyleType.Border;
                    field[10, 21] = TyleType.Border;
                }
            }
            else if (indFn == 6)
            {
                field[0, 24] = TyleType.Border;
                field[1, 24] = TyleType.Border;
                field[2, 24] = TyleType.Border;
                field[3, 24] = TyleType.Border;
                field[0, 23] = TyleType.Border;
                field[1, 23] = TyleType.Border;
                if (r.Next(0, 10) > 5)
                {
                    field[0, 22] = TyleType.Border;
                    field[1, 22] = TyleType.Border;
                    if (r.Next(0, 10) > 3)
                    {
                        field[0, 21] = TyleType.Border;
                        if (r.Next(0, 10) > 4)
                        {
                            field[0, 22] = TyleType.Border;
                        }
                    }
                }
                if (r.Next(0, 10) > 4)
                {
                    field[2, 23] = TyleType.Border;
                }
                field[10, 24] = TyleType.Border;
                field[9, 24] = TyleType.Border;
                if (r.Next(0, 10) > 2)
                {
                    field[10, 23] = TyleType.Border;
                    if (r.Next(0, 10) > 4)
                    {
                        field[10, 22] = TyleType.Border;
                    }
                    if (r.Next(0, 10) > 4)
                    {
                        field[8, 24] = TyleType.Border;
                    }
                }
            }
        }
        static void SetRoad()
        {
            if (roadType == 1)
            {
                field[indSp, 1] = TyleType.Road;
                field[indSp + 1, 2] = TyleType.Road;
                field[indSp + 1, 3] = TyleType.Road;
                if (r.Next(10) < 5)
                    field[indSp + 2, 4] = TyleType.Road;
                else
                    field[indSp + 1, 4] = TyleType.Road;
                field[indSp + 2, 5] = TyleType.Road;
                if (r.Next(10) < 5)
                    field[indSp + 2, 6] = TyleType.Road;
                else
                    field[indSp + 1, 6] = TyleType.Road;
                field[indSp + 1, 7] = TyleType.Road;
                field[indSp, 8] = TyleType.Road;
                field[5, 9] = TyleType.Road;
            }
            else
            {
                field[indSp, 1] = TyleType.Road;
                field[indSp, 2] = TyleType.Road;
                field[indSp - 1, 3] = TyleType.Road;
                field[indSp - 1, 4] = TyleType.Road;
                field[indSp - 2, 5] = TyleType.Road;
                if (indSp != 4)
                {
                    field[indSp - 3, 5] = TyleType.Road;
                    field[indSp - 4, 6] = TyleType.Road;
                    field[indSp - 3, 7] = TyleType.Road;

                }
                else
                {
                    field[indSp - 2, 5] = TyleType.Road;
                    field[indSp - 1, 6] = TyleType.Road;
                    field[indSp, 7] = TyleType.Road;
                }
                if (indSp == 5)
                    field[indSp - 2, 7] = TyleType.Road;
                field[5, 9] = TyleType.Road;
                field[4, 8] = TyleType.Road;
            }
            if (roadType == 1)
                roadType = r.Next(0, 2);
            if (roadType == -1)
            {
                field[6, 10] = TyleType.Road;
                field[7, 10] = TyleType.Road;
                field[8, 10] = TyleType.Road;
                field[9, 11] = TyleType.Road;
                field[9, 12] = TyleType.Road;
                field[8, 13] = TyleType.Road;
                if (r.Next(0, 10) < 5)
                    field[7, 13] = TyleType.Road;
                else
                    field[7, 14] = TyleType.Road;
                field[6, 14] = TyleType.Road;
                if (r.Next(0, 10) < 5)
                    field[5, 14] = TyleType.Road;
                else
                    field[5, 15] = TyleType.Road;
                field[4, 15] = TyleType.Road;

            }
            else if (roadType == 0)
            {
                field[4, 10] = TyleType.Road;
                field[3, 10] = TyleType.Road;
                field[2, 11] = TyleType.Road;
                field[1, 12] = TyleType.Road;
                field[1, 13] = TyleType.Road;
                if (r.Next(0, 10) < 5)
                    field[1, 14] = TyleType.Road;
                else
                    field[2, 14] = TyleType.Road;
                field[2, 15] = TyleType.Road;
                field[6, 10] = TyleType.Road;
                field[7, 11] = TyleType.Road;
                if (r.Next(0, 10) < 5)
                    field[7, 12] = TyleType.Road;
                else
                    field[8, 12] = TyleType.Road;
                field[8, 13] = TyleType.Road;
                field[8, 14] = TyleType.Road;
                field[7, 15] = TyleType.Road;
                field[6, 15] = TyleType.Road;
                field[5, 16] = TyleType.Road;
                field[4, 16] = TyleType.Road;
            }
            else if (roadType == 1)
            {
                field[4, 10] = TyleType.Road;
                field[3, 10] = TyleType.Road;
                if (r.Next(0, 10) < 5)
                    field[2, 11] = TyleType.Road;
                else
                    field[2, 10] = TyleType.Road;
                field[1, 11] = TyleType.Road;
                field[1, 12] = TyleType.Road;
                if (r.Next(0, 10) < 5)
                    field[1, 13] = TyleType.Road;
                else
                    field[2, 13] = TyleType.Road;
                field[2, 14] = TyleType.Road;
                if (r.Next(0, 10) < 5)
                    field[3, 15] = TyleType.Road;
                else
                    field[2, 15] = TyleType.Road;

            }
            field[3, 16] = TyleType.Road;
            field[indFn, 24] = TyleType.Road;
            field[5, 23] = TyleType.Road;
            FinishRoad();


        }
        static void SetBaseAndSpawn()
        {
            indSp = r.Next(4, 7);
            indFn = r.Next(4, 7);
            field[indSp, 0] = TyleType.Finish;
            field[indFn, 25] = TyleType.Start;
        }
        static void SetTyles()
        {
            Random r = new Random();
            int a;
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    if (field[i, j] == TyleType.Empty)
                    {
                        if (i != 0 && j != 0 && i != 10 && j != 25)
                        {
                            if (!IsNearBorder(i, j))
                            {
                                a = r.Next(260);
                                if (i == 1 || i == 9)
                                    a -= 60;
                                if (IsNearRoad(i, j))
                                    a -= 65;
                                if (a < 45)
                                    field[i, j] = TyleType.UsualTyle;

                            }
                        }
                        else
                        {
                            bool t = false;
                            a = r.Next(100);
                            if (i == 0) t = IsNearBorder(1, j);
                            else if (i == 10) t = IsNearBorder(9, j);
                            else if (j == 0) t = IsNearBorder(i, 10);
                            else if (j == 25) t = IsNearBorder(i, 24);
                            if (t)
                                a -= 50;
                            if (a < 48)
                                field[i, j] = TyleType.UsualTyle;
                        }
                    }

                }
            }
        }
        static bool IsNearBorder(int i, int j)
        {
            if (field[i - 1, j] == TyleType.Border || field[i + 1, j] == TyleType.Border)
                return true;
            if (field[i, j - 1] == TyleType.Border || field[i, j + 1] == TyleType.Border)
                return true;
            return false;
        }
        static bool IsNearRoad(int i, int j)
        {
            if (field[i - 1, j] == TyleType.Road || field[i + 1, j] == TyleType.Road)
                return true;
            if (field[i, j - 1] == TyleType.Road || field[i, j + 1] == TyleType.Road)
                return true;
            return false;
        }
        static void FinishRoad()
        {
            field[3, 17] = TyleType.Road;
            field[3, 18] = TyleType.Road;
            field[2, 19] = TyleType.Road;
            field[2, 20] = TyleType.Road;
            field[3, 21] = TyleType.Road;
            field[4, 21] = TyleType.Road;
            field[5, 22] = TyleType.Road;
        }
        static bool AnyRoadOnCorner(int i, int j)
        {
            return (field[i + 1, j + 1] == TyleType.Road || field[i - 1, j + 1] == TyleType.Road);
        }
        static void CorrectRoad()
        {
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    if (field[i, j] == TyleType.Road)
                    {
                        if (AnyRoadOnCorner(i, j) && field[i, j + 1] != TyleType.Border && field[i, j + 1] != TyleType.Finish)
                        {
                            field[i, j + 1] = TyleType.HelpingRoad;
                            Console.WriteLine("added road to " + i + "  " + j);
                        }
                    }
                }
            }
        }
        static void CorrectTyles()
        {
            List<Point> tyles = new List<Point>();
            List<Point> borders = new List<Point>();
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    if (field[i, j] == TyleType.UsualTyle)
                        tyles.Add(new Point(i, j));
                    if (field[i, j] == TyleType.Border)
                        borders.Add(new Point(i, j));
                }
            }
            double t = 286 - borders.Count;
            t = t / tyles.Count;
            int rand;
            if (t < 3.7)
            {
                for (int i = 0; i < 10; i++)
                {
                    rand = r.Next(10, tyles.Count);
                    field[tyles[rand].X, tyles[rand].Y] = TyleType.Empty;
                }

            }
            for (int i = 0; i < r.Next(3, 6); i++)
            {
                rand = r.Next(1, tyles.Count);
                field[tyles[rand].X, tyles[rand].Y] = TyleType.BigTyle;
            }
        }


    }

}
