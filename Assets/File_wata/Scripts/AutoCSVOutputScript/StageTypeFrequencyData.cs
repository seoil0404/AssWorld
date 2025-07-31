using System;
using UnityEngine;
using System.Collections.Generic;
using Wata.CSVData;

namespace Wata {

[GeneratedCode]
[Serializable]
public class StageTypeFrequencyData: CSVListData {
[field: SerializeField]
public Wata.MapGenerator.Stage Type { get; private set; }
[field: SerializeField]
public Int32 Frequency { get; private set; }
};

 }