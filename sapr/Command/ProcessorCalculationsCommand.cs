using sapr.Command.PreProcessorCommands;
using sapr.Stores;
using sapr.Stores.ProcessorStores;
using sapr.Utilities;
using sapr.ViewModels;
using sapr.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace sapr.Command
{
    public class ProcessorCalculationsCommand : CommandBase
    {
        public static event EventHandler ChangeState;

        private ProcecssorViewModel viewModel;
        private SuportStore SuportStore = SuportStore.Instance;
        private SmthStore SmthStore = SmthStore.Instance;
        private NodesStore NodesStore = NodesStore.Instance;
        private EPowerStore EPowerStore = EPowerStore.Instance;
        public static event Action calculated;
        
        public ProcessorCalculationsCommand(ProcecssorViewModel pvm)
        {
            viewModel = pvm;
        }

        public override void Execute(object parameter)
        {
            viewModel.MatrixA.Clear();
            viewModel.MatrixB.Clear();
            viewModel.VactorQ.Clear();
            viewModel.NX.Clear();
            viewModel.DX.Clear();
            viewModel.UX.Clear();
            viewModel.MyTextA = string.Empty;
            viewModel.MyTextB = string.Empty;
            viewModel.MyTextC = string.Empty;
            viewModel.MyTextD = string.Empty;
            viewModel.MyTextNX = string.Empty;
            viewModel.MyTextDX = string.Empty;
            viewModel.MyTextUX = string.Empty;
            viewModel.MyTextA = "Матрица А" + Environment.NewLine;
            viewModel.MyTextB = "Векстор B " + Environment.NewLine;
            viewModel.MyTextC = "Матрица ???" + Environment.NewLine;
            viewModel.MyTextD = "Дельта" + Environment.NewLine;
            viewModel.MyTextNX = "NX" + Environment.NewLine;
            viewModel.MyTextDX = "DX" + Environment.NewLine;
            viewModel.MyTextUX = "UX" + Environment.NewLine;
            


            CrrateMatixA();
            CreateMatrixB();
            CalculateDelta();
            CalculateNX();
            CalculateDX();
            CalculateUX();
            ChangeState?.Invoke(true, EventArgs.Empty);
            NXStore.Instance.SetUserData(viewModel.NX);
            UXStore.Instance.SetUserData(viewModel.UX);
            DXStore.Instance.SetUserData(viewModel.DX);
            QStore.Instance.SetUserData(viewModel.VactorQ);
            StepStore.Instance.SetUserData(viewModel.Step);
            PreProcessorComandBase.ResizeCanvas(9 + FindHihest.FindHeight(), 0);
            calculated.Invoke();
        }

        private void CalculateDX()
        {
            for (int i = 0; i < SuportStore.Instance.GetUserData().Count(); i++)
            {
                double X = SuportStore.Instance.GetUserData()[i].Model.Width ;
                if (SuportStore.Instance.GetUserData()[i].PrPower != 0)
                {
                    viewModel.DX.Add($"D{i + 1}(0): ", Math.Round((viewModel.NX[$"N{i + 1}(0): "] / SuportStore.Instance.GetUserData()[i].Model.Height), 3));
                    viewModel.DX.Add($"D{i + 1}(L): ", Math.Round((viewModel.NX[$"N{i + 1}(L): "] / SuportStore.Instance.GetUserData()[i].Model.Height), 3));
                }
                else
                    viewModel.DX.Add($"D{i + 1}(X): ", Math.Round((viewModel.NX[$"N{i + 1}(X): "] / SuportStore.Instance.GetUserData()[i].Model.Height), 3));
            }
            foreach (var item in viewModel.DX)
                viewModel.MyTextDX += " | " + item + Environment.NewLine;
        }
        public static double CalculateDX(int modelNumb, double x)
        {
            double X = x;
            return X / SuportStore.Instance.GetUserData()[modelNumb].Model.Height;
        }
        
        private void CalculateUX()
        {
            for (int i = 0; i < SuportStore.Instance.GetUserData().Count(); i++)
            {
                double X = SuportStore.Instance.GetUserData()[i].Model.Width ;

                if (SuportStore.Instance.GetUserData()[i].PrPower != 0)
                {
                    viewModel.UX.Add($"U{i + 1}(0): ", Math.Round((viewModel.VactorQ[i]),3));
                    viewModel.UX.Add($"U{i + 1}(L): ", Math.Round(((viewModel.VactorQ[i] + X / SuportStore.Instance.GetUserData()[i].Model.Width  * (viewModel.VactorQ[i + 1] - viewModel.VactorQ[i]) +
                                                        SuportStore.Instance.GetUserData()[i].PrPower * SuportStore.Instance.GetUserData()[i].Model.Width  * SuportStore.Instance.GetUserData()[i].Model.Width  / 2 * EPowerStore.Instance.GetUserData() *
                                                         SuportStore.Instance.GetUserData()[i].Model.Height * X / SuportStore.Instance.GetUserData()[i].Model.Width  *
                                                         (1 - X / SuportStore.Instance.GetUserData()[i].Model.Width ))), 3));
                }
                else
                {
                    if(i != 0 )
                        viewModel.UX.Add($"U{i + 1}(0): ", viewModel.UX[$"U{i}(L): "]);
                    else
                        viewModel.UX.Add($"U{i + 1}(0): ", 0);
                    viewModel.UX.Add($"U{i + 1}(L): ", Math.Round((viewModel.VactorQ[i] + X / SuportStore.Instance.GetUserData()[i].Model.Width * (viewModel.VactorQ[i + 1] - viewModel.VactorQ[i])), 3));

}               }
            foreach (var item in viewModel.UX)
                viewModel.MyTextUX += " | " + item + Environment.NewLine;
        }

        public static double CalculateUX(int modelNumb, double x)
        {
                double X = x;

                if (SuportStore.Instance.GetUserData()[modelNumb].PrPower != 0)
                {
                    return Math.Round(((QStore.Instance.GetUserData()[modelNumb] + X / SuportStore.Instance.GetUserData()[modelNumb].Model.Width * (QStore.Instance.GetUserData()[modelNumb + 1] - QStore.Instance.GetUserData()[modelNumb]) +
                                                        SuportStore.Instance.GetUserData()[modelNumb].PrPower * SuportStore.Instance.GetUserData()[modelNumb].Model.Width * SuportStore.Instance.GetUserData()[modelNumb].Model.Width / 2 * EPowerStore.Instance.GetUserData() *
                                                         SuportStore.Instance.GetUserData()[modelNumb].Model.Height * X / SuportStore.Instance.GetUserData()[modelNumb].Model.Width *
                                                         (1 - X / SuportStore.Instance.GetUserData()[modelNumb].Model.Width))), 3);
                }
                else
                {
                    return Math.Round((QStore.Instance.GetUserData()[modelNumb] + X / SuportStore.Instance.GetUserData()[modelNumb].Model.Width * (QStore.Instance.GetUserData()[modelNumb + 1] - QStore.Instance.GetUserData()[modelNumb])), 3);
                }
        }

        private void CalculateNX()
        {
            for (int i = 0; i < SuportStore.Instance.GetUserData().Count(); i++)
            {
                double X = SuportStore.Instance.GetUserData()[i].Model.Width;
                double E = EPowerStore.Instance.GetUserData();
                double A = SuportStore.Instance.GetUserData()[i].Model.Height;
                double L = SuportStore.Instance.GetUserData()[i].Model.Width;
                double Upo = viewModel.VactorQ[i];
                double Up1 = viewModel.VactorQ[i + 1];
                double q = SuportStore.Instance.GetUserData()[i].PrPower;

                if (SuportStore.Instance.GetUserData()[i].PrPower != 0)
                {
                    viewModel.NX.Add($"N{i + 1}(0): ", (E * A / L * (Up1 - Upo) + q * L / 2 * (1 - 2 * 0 / L)));
                    viewModel.NX.Add($"N{i + 1}(L): ", ((E * A) / L * (Up1 - Upo) + (q * L) / 2 * (1 - 2 * (X / L))));
                }
                else
                    viewModel.NX.Add($"N{i + 1}(X): ", (E * A) / L * (Up1 - Upo));
            }
            foreach (var item in viewModel.NX)
                viewModel.MyTextNX += " | " + item + Environment.NewLine;
        }

        public static double CalculateNX(int modelNumb, double x)
        {

                double X = x;
                double E = EPowerStore.Instance.GetUserData();
                double A = SuportStore.Instance.GetUserData()[modelNumb].Model.Height;
                double L = SuportStore.Instance.GetUserData()[modelNumb].Model.Width;
                double Upo = QStore.Instance.GetUserData()[modelNumb];
                double Up1 = QStore.Instance.GetUserData()[modelNumb + 1];
                double q = SuportStore.Instance.GetUserData()[modelNumb].PrPower;


            if (SuportStore.Instance.GetUserData()[modelNumb].PrPower != 0)
            {
                return ((E * A) / L * (Up1 - Upo) + (q * L) / 2 * (1 - 2 * (X / L)));
            }
            else
                return ((E * A) / L * (Up1 - Upo));

        }

        private void CalculateDelta()
        {
            int start = 0;
            int end = 0;
            if (SmthStore.Instance.GetUserData().X == 1)
                start++;
            if (SmthStore.Instance.GetUserData().Y == 1 && SuportStore.Instance.GetUserData().Count() > 1)
                end = viewModel.MatrixB.Count() - 2;
            else
                end = viewModel.MatrixB.Count() - 1;
            List<List<double>> MatrixRes = new List<List<double>>();
            List<double> VectorRes = new List<double>();
            for (int x = start; x <= end; x++)
            {
                MatrixRes.Add(new List<double>());
                for (int y = start; y <= end; y++)
                {
                    MatrixRes[x - start].Add(viewModel.MatrixA[x][y]);
                }
                if(SmthStore.Instance.GetUserData().Y == 0)
                    VectorRes.Add(viewModel.MatrixB[x]);
                else
                VectorRes.Add(viewModel.MatrixB[x]);
            }
            for (int e = 0; e < MatrixRes.Count(); e++)
            {
                for (int r = 0; r < MatrixRes.Count(); r++)
                {
                    viewModel.MyTextC += "   " + MatrixRes[e][r];
                }
                viewModel.MyTextC += " | " + VectorRes[e];
                viewModel.MyTextC += Environment.NewLine;
            }

            bool IsFind = false;
            double sub1 = 0;
            double sub2 = 0;

            for (int i = 0; i < MatrixRes.Count(); i++)
            {
                IsFind = false;
                int Count = 0;
                for (int j = 0; j < MatrixRes[i].Count(); j++)
                {

                    for (int k = 0; k < MatrixRes[i].Count(); k++)
                    {
                        if (i + 1 < MatrixRes.Count() && !IsFind)
                        {
                            if (MatrixRes[i][k] != 0 && MatrixRes[i + 1][j] != 0)
                            {
                                sub1 = MatrixRes[i][k];
                                sub2 = MatrixRes[i + 1][j];
                                IsFind = true;
                                break;
                            }
                        }
                    }
                    if (i + 1 < MatrixRes.Count())
                    {

                        MatrixRes[i + 1][j] = MatrixRes[i + 1][j] * sub1 + (-(MatrixRes[i][j] * sub2));
                    }
                }
                if (i + 1 < MatrixRes.Count())
                    VectorRes[i + 1] = -(VectorRes[i] * sub2 + (-(VectorRes[i + 1] * sub1)));
            }

            for (int i = 0; i < end; i++)
            {
                viewModel.VactorQ.Add(0);
            }
            
                viewModel.VactorQ[end - 1] = VectorRes[end - 1] / MatrixRes[end - 1][end - 1];
            for (int i = end - 2; i >= 0; i--)
            {
                viewModel.VactorQ[i] = ((VectorRes[i] + -MatrixRes[i][i + 1] * viewModel.VactorQ[i + 1]) / MatrixRes[i][i]);
            }
            if (SmthStore.Instance.GetUserData().X == 1)
            {
                viewModel.VactorQ.Insert(0, 0);
            }
            if (SmthStore.Instance.GetUserData().Y == 1)
            {
                viewModel.VactorQ.Add(0);
            }
            foreach (var elm in viewModel.VactorQ)
                viewModel.MyTextD += " Я люблю Чеканина " + Math.Round(elm,3) + Environment.NewLine;

        }

        private void CreateMatrixB()
        {
            for (int i = 0; i < NodesStore.GetUserData().Count; i++)
            {


                if (i == 0)
                    viewModel.MatrixB.Add(NodesStore.GetUserData()[i].PoPower + (SuportStore.GetUserData()[i].PrPower * SuportStore.GetUserData()[i].Model.Width / 2));
                else if (i == NodesStore.GetUserData().Count - 1)
                    viewModel.MatrixB.Add(NodesStore.GetUserData()[i].PoPower + (SuportStore.GetUserData()[i - 1].PrPower * SuportStore.GetUserData()[i - 1].Model.Width / 2));
                else
                    viewModel.MatrixB.Add(NodesStore.GetUserData()[i].PoPower + (SuportStore.GetUserData()[i].PrPower * SuportStore.GetUserData()[i].Model.Width / 2) + (SuportStore.GetUserData()[i-1].PrPower * SuportStore.GetUserData()[i-1].Model.Width / 2));
            }
            foreach (var item in viewModel.MatrixB)
                viewModel.MyTextB += " " + item + Environment.NewLine;
        }

        private void CrrateMatixA()
        {
            for (int q = 0; q <= SuportStore.GetUserData().Count; q++)
            {
                viewModel.MatrixA.Add(new List<double>());
                for (int w = 0; w <= SuportStore.GetUserData().Count; w++)
                {
                    viewModel.MatrixA[q].Add(0);
                }
            }

            for (int i = 0; i < SuportStore.GetUserData().Count; i++)
            {
                if (SuportStore.Instance.GetUserData().Count() == 1)
                {
                    if (SmthStore.GetUserData().X == 1)
                    {
                        viewModel.MatrixA[i][i] = 1;
                        viewModel.MatrixA[i][i + 1] = 0;
                        viewModel.MatrixA[i + 1][i] = 0;
                        viewModel.MatrixA[i + 1][i + 1] = Math.Round(((double)((SuportStore.GetUserData()[i].Model.Height * EPowerStore.GetUserData() / SuportStore.GetUserData()[i].Model.Width))), 3);
                    }else
                    {
                        viewModel.MatrixA[i][i] = Math.Round(((double)((SuportStore.GetUserData()[i].Model.Height * EPowerStore.GetUserData() / SuportStore.GetUserData()[i].Model.Width))), 3);
                        viewModel.MatrixA[i][i + 1] = 0;
                        viewModel.MatrixA[i + 1][i] = 0;
                        viewModel.MatrixA[i + 1][i + 1] = 1;
                    }
                }else
                {
                    if (i == 0)
                    {
                        if (SmthStore.GetUserData().X == 1)
                        {
                            viewModel.MatrixA[i][i] = 1;
                            viewModel.MatrixA[i][i + 1] = 0;
                            viewModel.MatrixA[i + 1][i] = 0;
                            viewModel.MatrixA[i + 1][i + 1] = Math.Round(((double)((SuportStore.GetUserData()[i].Model.Height * EPowerStore.GetUserData() / SuportStore.GetUserData()[i].Model.Width) + SuportStore.GetUserData()[i + 1].Model.Height * EPowerStore.GetUserData() / SuportStore.GetUserData()[i + 1].Model.Width)), 3);
                        }
                        else
                        {
                            viewModel.MatrixA[i][i] = Math.Round((double)((SuportStore.GetUserData()[i].Model.Height * EPowerStore.GetUserData() / SuportStore.GetUserData()[i].Model.Width)), 3);
                            viewModel.MatrixA[i][i + 1] = Math.Round(((double)(SuportStore.GetUserData()[i].Model.Height * EPowerStore.GetUserData() / SuportStore.GetUserData()[i].Model.Width) * -1), 3);
                            viewModel.MatrixA[i + 1][i] = Math.Round(((double)(SuportStore.GetUserData()[i].Model.Height * EPowerStore.GetUserData() / SuportStore.GetUserData()[i].Model.Width) * -1), 3);
                            viewModel.MatrixA[i + 1][i + 1] = Math.Round(((double)((SuportStore.GetUserData()[i].Model.Height * EPowerStore.GetUserData() / SuportStore.GetUserData()[i].Model.Width) + SuportStore.GetUserData()[i + 1].Model.Height * EPowerStore.GetUserData() / SuportStore.GetUserData()[i + 1].Model.Width)), 3);
                        }
                    }
                    else if (i == SuportStore.GetUserData().Count - 1)
                    {
                        if (SmthStore.GetUserData().Y == 1)
                            viewModel.MatrixA[i + 1][i + 1] = (1);
                        else
                        {
                            viewModel.MatrixA[i + 1][i + 1] = Math.Round((double)((SuportStore.GetUserData()[i].Model.Height * EPowerStore.GetUserData() / SuportStore.GetUserData()[i].Model.Width)), 3);
                            viewModel.MatrixA[i][i + 1] = Math.Round(((double)(SuportStore.GetUserData()[i].Model.Height * EPowerStore.GetUserData() / SuportStore.GetUserData()[i].Model.Width) * -1), 3);
                            viewModel.MatrixA[i + 1][i] = Math.Round(((double)(SuportStore.GetUserData()[i].Model.Height * EPowerStore.GetUserData() / SuportStore.GetUserData()[i].Model.Width) * -1), 3);
                        }

                    }
                    else
                    {
                        viewModel.MatrixA[i][i + 1] = Math.Round(((double)(SuportStore.GetUserData()[i].Model.Height * EPowerStore.GetUserData() / SuportStore.GetUserData()[i].Model.Width) * -1), 3);
                        viewModel.MatrixA[i + 1][i] = Math.Round(((double)(SuportStore.GetUserData()[i].Model.Height * EPowerStore.GetUserData() / SuportStore.GetUserData()[i].Model.Width) * -1), 3);
                        viewModel.MatrixA[i + 1][i + 1] = Math.Round(((double)((SuportStore.GetUserData()[i].Model.Height * EPowerStore.GetUserData() / SuportStore.GetUserData()[i].Model.Width) + SuportStore.GetUserData()[i + 1].Model.Height / SuportStore.GetUserData()[i + 1].Model.Width)), 3);
                    }
                }
            }
            for (int e = 0; e <= SuportStore.GetUserData().Count; e++)
            {
                for (int r = 0; r <= SuportStore.GetUserData().Count; r++)
                {
                    viewModel.MyTextA += "   " + viewModel.MatrixA[e][r];
                }
                viewModel.MyTextA += Environment.NewLine;
            }
        }
    }
}
