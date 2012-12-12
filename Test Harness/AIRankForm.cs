﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Backend;
using Backend.AI;

namespace Test_Harness
{
    public partial class AIRankForm : Form
    {
        private List<NodeInstance> nodes;


        private Random rand;

        public AIRankForm()
        {
            InitializeComponent();
        }
        
        private void AIRankForm_Load(object sender, EventArgs e)
        {
            nodes = new List<NodeInstance>();

            nodes.Add(new NodeInstance("James", 0, @"C:\Users\james\Documents", 5368709120, 2952790016));
            nodes.Add(new NodeInstance("Alan", 3, @"C:\Users\alan\Documents", 7516192768, 4831838208));
            nodes.Add(new NodeInstance("Tom", 7, @"C:\Users\tom\Documents", 2147483648, 2000083648));
            nodes.Add(new NodeInstance("Patrick", 5, @"C:\Users\patrick\Documents", 4294967296, 3221225472));
            nodes.Add(new NodeInstance("Shane", 2, @"C:\Users\shane\Documents", 21474836480, 16106127360));

            Dictionary<NodeInstance, double> rankedNodesDict = NodePrioritizer.PrioritizeNodes(nodes);

            foreach(KeyValuePair<NodeInstance, double> nodeRanking in rankedNodesDict)
            {
                lstNodes.Items.Add(nodeRanking.Key.Name + " - rank: " + Math.Round(nodeRanking.Value, 2).ToString());
            }
            rand = new Random((int)DateTime.Now.Ticks);

            timer1.Start();
        }

        private void UpdateNodeListBox()
        {
            lstNodes.Items.Clear();
            //lstNodes.Invalidate();
            
            Dictionary<NodeInstance, double> rankedNodesDict = NodePrioritizer.PrioritizeNodes(nodes);
            
            foreach (KeyValuePair<NodeInstance, double> nodeRanking in rankedNodesDict)
            {
                lstNodes.Items.Add(nodeRanking.Key.Name + " - rank: " + Math.Round(nodeRanking.Value, 2).ToString());
            }
        }

        private void UpdateLabels()
        {
            string bestNodeName = lstNodes.Items[0].ToString().Split(new char[1] { '-' })[0].Trim();
            foreach (NodeInstance node in nodes)
            {
                if (node.Name == bestNodeName)
                {
                    txtName.Text = node.Name;
                    txtCPU.Text = Math.Round(node.CPU_Utilization, 0).ToString() + "%";
                    txtNetwork.Text = Math.Round(node.UsedBandwidth / node.MaxBandwidth, 2).ToString() + "%";
                }
            }
            string worstNodeName = lstNodes.Items[lstNodes.Items.Count - 1].ToString().Split(new char[1] { '-' })[0].Trim();
            foreach (NodeInstance node in nodes)
            {
                if (node.Name == worstNodeName)
                {
                    txtWorstName.Text = node.Name;
                    txtWorstCPU.Text = Math.Round(node.CPU_Utilization, 0).ToString() + "%";
                    txtWorstNetwork.Text = Math.Round(node.UsedBandwidth / node.MaxBandwidth, 2).ToString() + "%";
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateCPU();
            UpdateUsedBandwith();
            UpdateNodeListBox();
            UpdateLabels();
        }

        private void UpdateCPU()
        {
            foreach (NodeInstance node in nodes)
            {
                //markov chain
                if (node.CPU_Utilization >= 0 && node.CPU_Utilization <= 0.1)
                {
                    double n = rand.NextDouble();
                    if (n > 0.8 && n <= 0.9)
                    {
                        node.CPU_Utilization = (double)rand.Next(51, 101) / 100;
                    }
                    else if (n > 0.9 && n <= 1)
                    {
                        node.CPU_Utilization = (double)rand.Next(11, 51) / 100;
                    }
                }
                else if (node.CPU_Utilization > 0.1 && node.CPU_Utilization <= 0.5)
                {
                    double n = rand.NextDouble();
                    if (n > 0.2 && n <= 0.3)
                    {
                        node.CPU_Utilization = (double)rand.Next(51, 101) / 100;
                    }
                    else if (n > 0.3 && n <= 1)
                    {
                        node.CPU_Utilization = (double)rand.Next(0, 11) / 100;
                    }
                }
                else if (node.CPU_Utilization > 0.5 && node.CPU_Utilization <= 1)
                {
                    double n = rand.NextDouble();
                    if (n > 0.5 && n <= 0.8)
                    {
                        node.CPU_Utilization = (double)rand.Next(0, 11) / 100;
                    }
                    else if (n > 0.8 && n <= 1)
                    {
                        node.CPU_Utilization = (double)rand.Next(11, 51) / 100;
                    }
                }
                else
                {
                    //initializing
                    node.CPU_Utilization = (double)rand.Next(0, 11) / 100;
                }
            }
            
        }

        private void UpdateUsedBandwith()
        {
            foreach (NodeInstance node in nodes)
            {
                double n = rand.NextDouble();
                if (n < 0.2)
                {
                    node.UsedBandwidth = rand.Next(3000000);
                }
            }
        }
    }
}
