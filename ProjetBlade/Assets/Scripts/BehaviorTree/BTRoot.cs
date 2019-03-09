using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTRoot : ABTNode {

    public void StartBT() {
        childNode.Tick();
    }

}
