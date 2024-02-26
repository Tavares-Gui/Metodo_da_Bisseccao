namespace IA;

public class DiffEvolution
{
    protected int NPop { get; set; }
    protected int Dimension { get; set; }
    protected double MutationMin { get; set; }
    protected double MutationMax { get; set; }
    protected double Recombination { get; set; }
    protected List<double[]> Bounds { get; set; }
    protected int BestIndividualIndex { get; set; }
    private double[] IndividualsFitness { get; set; }
    protected List<double[]> Individuals { get; set; }
    private double[] IndividualsRetrictions { get; set; }
    protected Func<double[], double> Fitness { get; set; }
    protected Func<double[], double> Restriction { get; set; }

    public DiffEvolution(
        Func<double[], double> fitness, List<double[]> bounds, Func<double[], double> restriction,
        int npop, double mutationMin = 0.5, double mutationMax = 0.9,
        double recombination = 0.8)
    {
        this.NPop = npop;
        this.Bounds = bounds;
        this.Fitness = fitness;
        this.Dimension = bounds.Count;
        this.MutationMin = mutationMin;
        this.MutationMax = mutationMax;
        this.Restriction = restriction;
        this.Recombination = recombination;
        Individuals = new List<double[]>(NPop);
        this.IndividualsRetrictions = new double[NPop];
        this.IndividualsFitness = new double[NPop];
    }

    private void GeneratePopulation()
    {
        int dimension = Dimension;

        for (int i = 0; i < NPop; i++)
        {
            Individuals.Add(new double[dimension]);

            for (int j = 0; j < dimension; j++)
                Individuals[i][j] = Utils.Rescale(Random.Shared.NextDouble(), Bounds[j][0], Bounds[j][1]);

            IndividualsRetrictions[i] = Restriction(Individuals[i]);
            IndividualsFitness[i] = IndividualsRetrictions[i] <= 0.0 ? Fitness(Individuals[i]) : double.MaxValue;
        }

        FindBestIndividual();
    }

    private void FindBestIndividual()
    {
        var fitnessBest = IndividualsFitness[BestIndividualIndex];

        for (int i = 0; i < NPop; i++)
        {
            var fitnessCurrent = Fitness(Individuals[i]);

            if (fitnessCurrent < fitnessBest)
            {
                BestIndividualIndex = i;
                fitnessBest = fitnessCurrent;
            }
        }

        IndividualsFitness[BestIndividualIndex] = fitnessBest;
    }

    private double[] Mutate(int index)
    {
        int individualRand1, individualRand2;

        do individualRand1 = Random.Shared.Next(NPop);
        while (individualRand1 == index);

        do individualRand2 = Random.Shared.Next(NPop);
        while (individualRand2 == individualRand1);

        var newIndividual = (double[])Individuals[BestIndividualIndex].Clone();

        for (int i = 0; i < Dimension; i++)
        {
            newIndividual[i] += Utils.Rescale(Random.Shared.NextDouble(), MutationMin, MutationMax)
            * (Individuals[individualRand1][i] - Individuals[individualRand2][i]);
        }

        return newIndividual;
    }

    protected double[] Crossover(int index)
    {

        var trial = Mutate(index);
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

    protected void Iterate()
    {
        for (int i = 0; i < NPop; i++)
        {
            var trial = Crossover(i);

            var restTrial = Restriction(trial);
            var restIndividual = IndividualsRetrictions[i];
            double fitnessTrail = restTrial <= 0.0 ? Fitness(trial) : double.MaxValue;


            if (
                (restIndividual > 0.0 && restTrial < restIndividual)
                || (restTrial <= 0.0 && restIndividual > 0.0)
                || (restTrial <= 0.0 && fitnessTrail < IndividualsFitness[i])
            )
            {
                Individuals[i] = trial;
                IndividualsRetrictions[i] = restTrial;
                IndividualsFitness[i] = fitnessTrail;
            }
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