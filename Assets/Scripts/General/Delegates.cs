﻿using SimpleJSON;

public delegate void UpdateEntity(float dt);
public delegate void OnResourceValueChanged(float currentValue, float maxValue);
public delegate void OnCommandFinished(UnitEntity owner, UnitEntity[] target);
public delegate CommandAction CreateCommand(JSONNode data);