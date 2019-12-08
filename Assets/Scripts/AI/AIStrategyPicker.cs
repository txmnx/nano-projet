using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AIStrategyType
{
    PROACTIVE,
    REACTIVE
}

public class AIStrategyPicker
{
    public static Dictionary<AIStrategyType, IAIStrategy> strategies = new Dictionary<AIStrategyType, IAIStrategy>() {
        { AIStrategyType.PROACTIVE, new ProActiveStrategy() },
        { AIStrategyType.REACTIVE, new ReActiveStrategy() }
    };

    public static IAIStrategy RandomStrategy(System.Random random)
    {
        return strategies[(AIStrategyType)random.Next(0, strategies.Count)].NewInstance();
    }
}
