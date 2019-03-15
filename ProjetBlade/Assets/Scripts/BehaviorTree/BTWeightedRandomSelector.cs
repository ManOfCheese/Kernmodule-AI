using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The weighted random selector selects which node to trigger based on a weighted random chance (like 80/20). It returns succes if this node succeeds.
public class BTWeightedRandomSelector : ABTNode {

    public List<ABTNode> childNodes;
    public List<float> chanceWeights; //Must add up to 1;

    public override TaskState Tick() {
        float randomResult = Random.Range(0.0f, 1.0f);
        for (int i = 0; i < childNodes.Count; i++) {
            if (i == 0) {
                if (randomResult < chanceWeights[i]) {
                    //Debug.Log("WeightedRandomSelector || " + childNodes[i].Tick());
                    return childNodes[i].Tick();
                }
            }
            else {
                if (randomResult > chanceWeights[i - 1] && randomResult < chanceWeights[i]) {
                    //Debug.Log("WeightedRandomSelector || " + childNodes[i].Tick());
                    return childNodes[i].Tick();
                }
            }
        }
        Debug.Log("WeightedRandomSelector || No node was ticked");
        return TaskState.Failure;
    }
}
