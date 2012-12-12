using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AI.Fuzzy.Library;

namespace Backend.AI
{
    public static class NodePrioritizer
    {
        public static Dictionary<NodeInstance, double> PrioritizeNodes(List<NodeInstance> nodes)
        {
            MamdaniFuzzySystem fsNodeSys = new MamdaniFuzzySystem();

            FuzzyVariable fvCPU = new FuzzyVariable("cpu", 0.0, 1);
            fvCPU.Terms.Add(new FuzzyTerm("low", new TriangularMembershipFunction(-.50, 0, .50)));
            fvCPU.Terms.Add(new FuzzyTerm("med", new TriangularMembershipFunction(0, .50, 1)));
            fvCPU.Terms.Add(new FuzzyTerm("high", new TriangularMembershipFunction(.50, 1, 1.5)));
            fsNodeSys.Input.Add(fvCPU);

            FuzzyVariable fvBandwidth = new FuzzyVariable("bandwidth", 0.0, 1);
            fvBandwidth.Terms.Add(new FuzzyTerm("low", new TriangularMembershipFunction(-.50, 0, .50)));
            fvBandwidth.Terms.Add(new FuzzyTerm("med", new TriangularMembershipFunction(0, .50, 1)));
            fvBandwidth.Terms.Add(new FuzzyTerm("high", new TriangularMembershipFunction(.50, 1, 1.5)));
            fsNodeSys.Input.Add(fvBandwidth);

            FuzzyVariable fvFreeSpace = new FuzzyVariable("freespace", 0.0, 1);
            fvFreeSpace.Terms.Add(new FuzzyTerm("low", new TriangularMembershipFunction(-.5, 0, .5)));
            fvFreeSpace.Terms.Add(new FuzzyTerm("moderate", new TriangularMembershipFunction(0, .5, 1)));
            fvFreeSpace.Terms.Add(new FuzzyTerm("ample", new TriangularMembershipFunction(.5, 1, 1.5)));
            fsNodeSys.Input.Add(fvFreeSpace);

            //
            // Create output variables for the system
            //
            FuzzyVariable fvRank = new FuzzyVariable("rank", 0, 1);
            fvRank.Terms.Add(new FuzzyTerm("low", new TriangularMembershipFunction(-0.25, 0, 0.25)));
            fvRank.Terms.Add(new FuzzyTerm("med_low", new TriangularMembershipFunction(0, 0.25, 0.50)));
            fvRank.Terms.Add(new FuzzyTerm("med", new TriangularMembershipFunction(0.25, 0.50, 0.75)));
            fvRank.Terms.Add(new FuzzyTerm("med_high", new TriangularMembershipFunction(0.50, 0.75, 1)));
            fvRank.Terms.Add(new FuzzyTerm("high", new TriangularMembershipFunction(0.75, 1, 1.25)));

            fsNodeSys.Output.Add(fvRank);

            MamdaniFuzzyRule rule1 = fsNodeSys.ParseRule("if (cpu is low) and (bandwidth is high) and (freespace is ample) then rank is med");
            MamdaniFuzzyRule rule2 = fsNodeSys.ParseRule("if (cpu is low) and (bandwidth is high) and (freespace is moderate) then rank is med_low");
            MamdaniFuzzyRule rule3 = fsNodeSys.ParseRule("if (cpu is low) and (bandwidth is high) and (freespace is low) then rank is low");
            MamdaniFuzzyRule rule4 = fsNodeSys.ParseRule("if (cpu is low) and (bandwidth is med) and (freespace is ample) then rank is med_high");
            MamdaniFuzzyRule rule5 = fsNodeSys.ParseRule("if (cpu is low) and (bandwidth is med) and (freespace is moderate) then rank is med_high");
            MamdaniFuzzyRule rule6 = fsNodeSys.ParseRule("if (cpu is low) and (bandwidth is med) and (freespace is low) then rank is med_low");
            MamdaniFuzzyRule rule7 = fsNodeSys.ParseRule("if (cpu is low) and (bandwidth is low) and (freespace is ample) then rank is high");
            MamdaniFuzzyRule rule8 = fsNodeSys.ParseRule("if (cpu is low) and (bandwidth is low) and (freespace is moderate) then rank is med_high");
            MamdaniFuzzyRule rule9 = fsNodeSys.ParseRule("if (cpu is low) and (bandwidth is low) and (freespace is low) then rank is low");
            MamdaniFuzzyRule rule10 = fsNodeSys.ParseRule("if (cpu is med) and (bandwidth is high) and (freespace is ample) then rank is med");
            MamdaniFuzzyRule rule11 = fsNodeSys.ParseRule("if (cpu is med) and (bandwidth is high) and (freespace is moderate) then rank is med_low");
            MamdaniFuzzyRule rule12 = fsNodeSys.ParseRule("if (cpu is med) and (bandwidth is high) and (freespace is low) then rank is low");
            MamdaniFuzzyRule rule13 = fsNodeSys.ParseRule("if (cpu is med) and (bandwidth is med) and (freespace is ample) then rank is med");
            MamdaniFuzzyRule rule14 = fsNodeSys.ParseRule("if (cpu is med) and (bandwidth is med) and (freespace is moderate) then rank is med");
            MamdaniFuzzyRule rule15 = fsNodeSys.ParseRule("if (cpu is med) and (bandwidth is med) and (freespace is low) then rank is low");
            MamdaniFuzzyRule rule16 = fsNodeSys.ParseRule("if (cpu is med) and (bandwidth is low) and (freespace is ample) then rank is med_high");
            MamdaniFuzzyRule rule17 = fsNodeSys.ParseRule("if (cpu is med) and (bandwidth is low) and (freespace is moderate) then rank is med_high");
            MamdaniFuzzyRule rule18 = fsNodeSys.ParseRule("if (cpu is med) and (bandwidth is low) and (freespace is low) then rank is low");
            MamdaniFuzzyRule rule19 = fsNodeSys.ParseRule("if (cpu is high) and (bandwidth is high) and (freespace is ample) then rank is med");
            MamdaniFuzzyRule rule20 = fsNodeSys.ParseRule("if (cpu is high) and (bandwidth is high) and (freespace is moderate) then rank is med_low");
            MamdaniFuzzyRule rule21 = fsNodeSys.ParseRule("if (cpu is high) and (bandwidth is high) and (freespace is low) then rank is low");
            MamdaniFuzzyRule rule22 = fsNodeSys.ParseRule("if (cpu is high) and (bandwidth is med) and (freespace is ample) then rank is med");
            MamdaniFuzzyRule rule23 = fsNodeSys.ParseRule("if (cpu is high) and (bandwidth is med) and (freespace is moderate) then rank is med_low");
            MamdaniFuzzyRule rule24 = fsNodeSys.ParseRule("if (cpu is high) and (bandwidth is med) and (freespace is low) then rank is low");
            MamdaniFuzzyRule rule25 = fsNodeSys.ParseRule("if (cpu is high) and (bandwidth is low) and (freespace is ample) then rank is med_low");
            MamdaniFuzzyRule rule26 = fsNodeSys.ParseRule("if (cpu is high) and (bandwidth is low) and (freespace is moderate) then rank is med");
            MamdaniFuzzyRule rule27 = fsNodeSys.ParseRule("if (cpu is high) and (bandwidth is low) and (freespace is low) then rank is low");

            fsNodeSys.Rules.Add(rule1);
            fsNodeSys.Rules.Add(rule2);
            fsNodeSys.Rules.Add(rule3);
            fsNodeSys.Rules.Add(rule4);
            fsNodeSys.Rules.Add(rule5);
            fsNodeSys.Rules.Add(rule6);
            fsNodeSys.Rules.Add(rule7);
            fsNodeSys.Rules.Add(rule8);
            fsNodeSys.Rules.Add(rule9);
            fsNodeSys.Rules.Add(rule10);
            fsNodeSys.Rules.Add(rule11);
            fsNodeSys.Rules.Add(rule12);
            fsNodeSys.Rules.Add(rule13);
            fsNodeSys.Rules.Add(rule14);
            fsNodeSys.Rules.Add(rule15);
            fsNodeSys.Rules.Add(rule16);
            fsNodeSys.Rules.Add(rule17);
            fsNodeSys.Rules.Add(rule18);
            fsNodeSys.Rules.Add(rule19);
            fsNodeSys.Rules.Add(rule20);
            fsNodeSys.Rules.Add(rule21);
            fsNodeSys.Rules.Add(rule22);
            fsNodeSys.Rules.Add(rule23);
            fsNodeSys.Rules.Add(rule24);
            fsNodeSys.Rules.Add(rule25);
            fsNodeSys.Rules.Add(rule26);
            fsNodeSys.Rules.Add(rule27);

            var rankedNodes = new Dictionary<NodeInstance, double>();

            for (int i = 0; i < nodes.Count; i++)
            {


                //
                // Fuzzify input values
                //
                Dictionary<FuzzyVariable, double> inputValues = new Dictionary<FuzzyVariable, double>();
                inputValues.Add(fvCPU, nodes[i].CPU_Utilization);
                inputValues.Add(fvBandwidth, nodes[i].UsedBandwidth / nodes[i].MaxBandwidth);
                inputValues.Add(fvFreeSpace, nodes[i].FreeSpace / nodes[i].MaxBackupSpace);
                
                //
                // Calculate the result
                //
                Dictionary<FuzzyVariable, double> result = fsNodeSys.Calculate(inputValues);

                double rank = result[fvRank];
                rankedNodes.Add(nodes[i], rank);

                //Console.WriteLine(nodes[i].ToString());
                //Console.WriteLine("Rank: " + Math.Round(rank, 2).ToString());
                //Console.WriteLine();
            }
            var sortedNodes 
                = (from entry in rankedNodes orderby entry.Value ascending select entry)
                .ToDictionary(pair => pair.Key, pair => pair.Value);
            return sortedNodes;



        }
    }
}
