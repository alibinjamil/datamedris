using System;
using System.Runtime.Serialization;


namespace RIS.RISVoiceControl.AudioLibrary
{
	/// <summary>
	/// Config information for MP3 writer
	/// </summary>
  [Serializable]
	public class Mp3WriterConfig : AudioWriterConfig
	{
    private BE_CONFIG m_BeConfig;

    protected Mp3WriterConfig(SerializationInfo info, StreamingContext context)
      :base(info, context)
    {
      m_BeConfig = (BE_CONFIG)info.GetValue("BE_CONFIG", typeof(BE_CONFIG));
    }

    public Mp3WriterConfig(WaveFormat InFormat, BE_CONFIG beconfig)
      :base(InFormat)
    {
      m_BeConfig = beconfig;
    }

    public Mp3WriterConfig(WaveFormat InFormat)
      :this(InFormat, new BE_CONFIG(InFormat))
    {
    }

    public Mp3WriterConfig()
      :this(new WaveFormat(44100, 16, 2))
		{
		}
	
    public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
    {
      base.GetObjectData(info, context);
      info.AddValue("BE_CONFIG", m_BeConfig, m_BeConfig.GetType());
    }

    public BE_CONFIG Mp3Config
    {
      get
      {
        return m_BeConfig;
      }
      set
      {
        m_BeConfig = value;
      }
    }
  }
}
