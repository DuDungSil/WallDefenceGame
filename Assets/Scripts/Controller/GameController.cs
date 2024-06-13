
public class GameController : Singleton<GameController>
{

    public ToolItemData[] starterKit;


    void Start()
    {
        OptionManager.Instance.InitAudioMixer();

        for(int i = 0; i < starterKit.Length; i++)
        {
            EquipmentDatabase.Instance.AddItem((EquipmentItem)starterKit[i].CreateItem(), 3);
        }

        //SoundController.Instance.PlaySound2D("tavern loop", 0, true, SoundType.BGM);
    }


    void Update()
    {

        
    }

}
