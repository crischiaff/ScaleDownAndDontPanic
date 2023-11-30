using System.Collections;
using System.Collections.Generic;

public class SelectedFeatures
{
    public List<Feature> features;


    public SelectedFeatures()
    {
        features = new List<Feature>();
    }

    public void AddFeature(Feature feature)
    {
        features.Add(feature);
    }

    public void RemoveFeature(Feature feature)
    {
        features.Remove(feature);
    }

    public bool Contains(Feature feature)
    {
        return features.Contains(feature);
    }
}
