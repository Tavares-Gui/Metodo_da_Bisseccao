namespace IA;

public class DiffEvolution
{
    protected int NPop { get; set; }
    protected int Dimension { get; set; }
    protected double Mutation { get; set; }
    protected double Recombination { get; set; }
    protected List<double[]> Bounds { get; set; }
    protected int BestIndividualIndex { get; set; }
    protected List<double[]> Individuals { get; set; }
    protected Func<double[], double> Fitness { get; set; }
    protected double BestIndividualFitness { get; set; } = double.MaxValue;

    public DiffEvolution(Func<double[], double> fitness, List<double[]> bounds, int npop, double mutation = 0.7, double recombination = 0.8)
    {
        this.NPop = npop;
        this.Bounds = bounds;
        this.Fitness = fitness;
        this.Mutation = mutation;
        this.Dimension = bounds.Count;
        this.Recombination = recombination;
        Individuals = new List<double[]>(NPop);
    }

    private void GeneratePopulation()
    {
        int dimension = Dimension;

        for (int i = 0; i < NPop; i++)
        {
            Individuals.Add(new double[dimension]);

            for (int j = 0; j < dimension; j++)
                Individuals[i][j] = Utils.Rescale(Random.Shared.NextDouble(), Bounds[j][0], Bounds[j][1]);
        }

        FindBestIndividual();
    }

    private void FindBestIndividual()
    {
        var fitnessBest = BestIndividualFitness;
        
        for (int i = 0; i < NPop; i++)
        {
            var fitnessCurrent = Fitness(Individuals[i]);

            if (fitnessCurrent < fitnessBest)
            {
                BestIndividualIndex = i;
                fitnessBest = fitnessCurrent;
            }
        }

        BestIndividualFitness = fitnessBest;
    }

    private double[] Mutate(double[] individual)
    {
        var newIndividual = new double[Dimension]; 
        
        var individualRand1 = Random.Shared.Next(NPop);
        int individualRand2;

        do
        {
            individualRand2 = Random.Shared.Next(NPop);
        } while (individualRand2 == individualRand1);
        
        for (int i = 0; i < Dimension; i++)
        {
            newIndividual[i] += Mutation * Individuals[individualRand1][i] - Individuals[individualRand2][i];
        }

        return individual;
    }

    protected double[] Crossover(int index)
    {
        
        var trial = Mutate(Individuals[index]);
        var trial2 = (double[])Individuals[index].Clone();

        for (int i = 0; i < Dimension; i++)
        {
            if (!(
                  Random.Shared.NextDouble() < Recombination ||
                  i == Random.Shared.Next(Dimension)
                ))
                trial2[i] = trial[i];
        }

        return trial2;
    }

    protected void Iterate() {
        for (int i = 0; i < NPop; i++)
        {
            var trial = Crossover(i);

            if (Fitness(trial) < Fitness(Individuals[i]))
                Individuals[i] = trial;
        }

        FindBestIndividual();
    }

    public double[] Optimize(int n)
    {
        GeneratePopulation();

        for (int i = 0; i < n; i++)
            Iterate();

        return Individuals[BestIndividualIndex];
    }
}