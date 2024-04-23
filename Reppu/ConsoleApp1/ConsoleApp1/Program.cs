public class tavara
{
    protected double paino;
    protected double tilavuus;

    public double Paino
    {
        get { return paino; }
        set { paino = value; }
    }

    public double Tilavuus
    {
        get { return tilavuus; }
        set {  tilavuus = value; }
    }

    public tavara(double paino, double tilavuus)
    {
        this.paino = paino;
        this.tilavuus = tilavuus;
    }
}

public class reppu
{
    private double maxPaino;
    private double maxTilavuus;
    private tavara[] sisältö;

    public reppu(double maxPaino, double maxTilavuus)
    {
        this.maxPaino = maxPaino;
        this.maxTilavuus = maxTilavuus;
    }
}

public class nuoli : tavara
{
    public nuoli(double paino, double tilavuus) : base(0.1, 0.05) { }
}

public class jousi : tavara
{
    public jousi(double paino, double tilavuus) : base(1, 4) { }
}

public class köysi : tavara
{
    public köysi(double paino, double tilavuus) : base(1, 1.5) { }
}

public class vesi : tavara
{
    public vesi(double paino, double tilavuus) : base(2, 2) { }
}

public class ruokaannos : tavara
{
    public ruokaannos(double paino, double tilavuus) : base(1, 0.5) { }
}

public class miekka : tavara
{
    public miekka(double paino, double tilavuus) : base(5, 3) { }
}