

namespace GameEngine;

public class Stat : IStat
{
    
    protected int value;

    
    protected int baseValue;

    
    protected int minValue;

    
    protected int maxValue;

    protected Stat() { }

    /// <summary>
    /// Represents a Card's property.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="baseValue"></param>
    /// <param name="minValue"></param>
    /// <param name="maxValue"></param>
    public Stat(int value, int baseValue, int minValue = int.MinValue, int maxValue = int.MaxValue)
    {
        this.baseValue = baseValue;
        this.value = value;
        this.minValue = minValue;
        this.maxValue = maxValue;
    }

    
    public virtual int Value
    {
        get => value;
        set => this.value = Math.Max(minValue, Math.Min(maxValue, value));
    }

    
    public virtual int BaseValue
    {
        get => baseValue;
        set => baseValue = Math.Max(minValue, Math.Min(maxValue, value));
    }

    
    public virtual int MinValue
    {
        get => minValue;
        set => this.minValue = value;
    }

    
    public virtual int MaxValue
    {
        get => maxValue;
        set => this.maxValue = value;
    }

    public virtual void Invert()
    {
        Value = -Value;
        BaseValue = -BaseValue;
    }
}
