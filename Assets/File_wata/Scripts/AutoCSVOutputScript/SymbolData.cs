using System;
using UnityEngine;
using System.Collections.Generic;
using Wata.CSVData;

namespace Wata {

[GeneratedCode]
[Serializable]
public class SymbolData: CSVDictionaryData {
[field: SerializeField]
public String Name { get; private set; }
[field: SerializeField]
public String ConditionCode { get; private set; }
[field: SerializeField]
public String Condition { get; private set; }
[field: SerializeField]
public String Description { get; private set; }
[field: SerializeField]
public Wata.SymbolRarity Rarity { get; private set; }
[field: SerializeField]
public String Effect { get; private set; }
[field: SerializeField]
public Int32 ProcessPriority { get; private set; }
[field: SerializeField]
public Wata.SymbolType Type { get; private set; }
[field: SerializeField]
public Wata.SymbolCategory Category { get; private set; }
};

 }