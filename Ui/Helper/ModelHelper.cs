using Newtonsoft.Json;

namespace Ui.Helper;


public static class ModelHelper
{
    public static ModelType? ParseJson<ModelType>(Stream stream)
    {
        using var reader = new StreamReader(stream);
        var json = reader.ReadToEnd();
        var model = JsonConvert.DeserializeObject<ModelType>(json);
        return model;
    }
}
