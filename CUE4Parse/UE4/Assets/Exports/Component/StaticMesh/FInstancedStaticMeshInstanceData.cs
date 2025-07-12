using CUE4Parse.UE4.Objects.Core.Math;
using CUE4Parse.UE4.Readers;
using CUE4Parse.UE4.Versions;

namespace CUE4Parse.UE4.Assets.Exports.Component.StaticMesh;

public class FInstancedStaticMeshInstanceData
{
    public readonly FMatrix Transform;

    public readonly FTransform TransformData = new();

    public FInstancedStaticMeshInstanceData(FArchive Ar)
    {
        Transform = new FMatrix(Ar);

        Ar.Position += Ar.Game switch
        {
            EGame.GAME_HogwartsLegacy => Ar.Read<int>() * sizeof(int),
            EGame.GAME_AWayOut or EGame.GAME_PlayerUnknownsBattlegrounds or EGame.GAME_SeaOfThieves
                or EGame.GAME_DaysGone or EGame.GAME_InfinityNikki or EGame.GAME_NarutotoBorutoShinobiStriker => 16, // sizeof(FVector2D) * 2; LightmapUVBias, ShadowmapUVBias
            EGame.GAME_SilentHill2Remake or EGame.GAME_StateOfDecay2 => 32,// probably LightmapUVBias, ShadowmapUVBias as FVector2d * 2
            _ => 0,
        };
        TransformData.SetFromMatrix(Transform);
    }

    public override string ToString()
    {
        return TransformData.ToString();
    }
}
