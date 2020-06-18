﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor; //todo: ifdef this
#endif
namespace UMA
{
	[Serializable]
	public class UMAPredefinedDNA
	{
		/// <summary>
		/// This class is used for preloading DNA on DynamicCharacterAvatars  
		/// </summary>
		/// <remarks>
		/// UMAPredefinedDNA is assigned to a DynamicCharacterAvatar to allow it to preload DNA when it is built.
		/// Without this, it is not possible to pregenerate random DNA because the DNA is loaded from the Race at build time.
		/// After the UMAData is created, this DNA will be applied to the UMA as part of the build process, so you don't have
		/// to build the DCA twice to get randomized data/.
		/// </remarks>
		[Serializable]
	    public class DnaValue
		{
			public string Name;
			public float Value;
			public DnaValue(string name, float value)
			{
				Name = name;
				Value = value;
			}
		}

		public List<DnaValue> PreloadValues = new List<DnaValue>();

		public void AddDNA(string Name, float Value)
		{
			PreloadValues.Add(new DnaValue(Name, Value));
		}
	}
}
