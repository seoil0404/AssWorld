﻿using UnityEngine;

namespace Wata.CSVData {
    public abstract class CSVListData {}
 
    public abstract class CSVDictionaryData: CSVListData {
     
        [field: SerializeField]
        public int SerialNumber { get; protected set; }
    }
   
}

