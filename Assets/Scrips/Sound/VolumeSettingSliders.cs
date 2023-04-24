using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSettingSliders : MonoBehaviour
{
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider seSlider;
    [SerializeField] private GameObject ctrlerPrefab;
    private VolumeController ctrler;

    // Start is called before the first frame update
    public void StartSetting()
    {
        ctrler = Instantiate(ctrlerPrefab).GetComponent<VolumeController>();

        seSlider.SetValueWithoutNotify(SoundManager.I.SeVolume);
        bgmSlider.value = SoundManager.I.BgmVolume;

        //SoundManager2.I.StartVolumeSettnig();
    }

    public void OnBGMValueChanged()
    {
        ctrler.BGMVolume = bgmSlider.value;
    }
    
    public void OnSEValueChanged()
    {
        ctrler.PlaySeSettingClip();
       
        ctrler.SEVolume = seSlider.value;
    }
    
}
