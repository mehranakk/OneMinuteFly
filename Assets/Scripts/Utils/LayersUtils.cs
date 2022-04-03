using System.Collections;
using System.Collections.Generic;


public enum LayersEnum
{
    NONE,
    DEFAULT,
    PLAYER,
    ENEMIES,
    WATER,
}
public static class LayersUtils
{
    public static LayersEnum LayerNameToLayerEnum(string layerName)
    {
        switch (layerName){
            case "Default":
                return LayersEnum.DEFAULT;
            case "Player":
                return LayersEnum.PLAYER;
            case "Enemies":
                return LayersEnum.ENEMIES;
            case "Water":
                return LayersEnum.WATER;
        }
        return LayersEnum.NONE;
    }

    public static string LayerEnumToLayerName(LayersEnum layerEnum)
    {
        switch (layerEnum){
            case LayersEnum.DEFAULT:
                return "Default";
            case LayersEnum.PLAYER:
                return "Player";
            case LayersEnum.ENEMIES:
                return "Enemies";
            case LayersEnum.WATER:
                return "Water";
        }
        return "InvalidEnum";
    }
    public static LayersEnum LayerNumberToLayerEnum(int layerNumber)
    {
        switch (layerNumber){
            case 0:
                return LayersEnum.DEFAULT;
            case 4:
                return LayersEnum.WATER;
            case 7:
                return LayersEnum.PLAYER;
            case 8:
                return LayersEnum.ENEMIES;
        }
        return LayersEnum.NONE;
    }
}
