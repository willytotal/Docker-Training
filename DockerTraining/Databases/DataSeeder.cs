namespace DockerTraining.Databases;

internal static class DataSeeder
{
    public static DisplayEntity[] DisplaySeeds()
    {
        return new[]
        {
            new DisplayEntity { DisplayId = 1, Description = "Display 1", Created = new DateTime(2023, 6, 1) },
            new DisplayEntity { DisplayId = 2, Description = "Display 2", Created = new DateTime(2021, 4, 1) }
        };
    }

}
