

using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Text;

var value = new
{
    provider_id = "RQKImu08QI",
    menu_item_availability_updates = new List<Items>() 
    { new Items() 
    {
        sku = "{\"id\":\"1010442\",\"isCombo\":true,\"isSplitLines\":false}",
        transition_to = "out_of_stock",
        transition_timestamp = 1741159432
    } },
};

var adf = Utf8Json.JsonSerializer.ToJsonString(value, Utf8Json.Resolvers.StandardResolver.ExcludeNullCamelCase);
var bitesofRequest = Encoding.UTF8.GetBytes(adf);
var bitesofKey = Encoding.UTF8.GetBytes("");

var sig = adf.ToBytes().HmacSha256Base64(Bolt.SecretKey.ToBytes());







class Items
{
    public string sku { get; set; }
    public string transition_to { get; set; }
    public long transition_timestamp { get; set; }
}