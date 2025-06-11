using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class game : MonoBehaviour
{
	//游戏状态
	public enum GAME_STATUS 
	{ 
		Ready,
		InGame,
		GameOver,
	}
	private GAME_STATUS status;
	private GAME_STATUS Status
	{
		get { return status; }
		set
		{
			this.status = value;
			this.UpdateUI();
		}
	}
	//面板状态
	public GameObject PanelReady;
	public GameObject PanelInGame;
	public GameObject PanelGameOver;

	//管道管理器
	public PipelineManager pipelineManager;
	//音效管理器
	public MusicManager musicManager;
	//小鸟实体
	public Player player;

	//分数
	private int scoreBest;
	public int score;
	public Text uiScore;
	public Text uiScoreEnd;
	public Text uiScoreBest;
	public int Scroe
	{
		get { return score; }
		set
		{
			this.score = value;
			this.uiScore.text = this.score.ToString();
			this.uiScoreEnd.text = this.score.ToString();
		}
	}

	public void OnPlayerScore(int getScore)
	{
		this.Scroe += getScore;
	}

	// Use this for initialization
	void Start ()
	{
		//游戏开始时进入准备状态
		this.PanelReady.SetActive(true);
		//小鸟刚体失效，以保持漂浮状态
		this.player.rigidbodyBird.Sleep();
		//获取小鸟初始位置，供重新开始时小鸟位置初始化使用
		this.player.initPos = player.transform.position;
		//订阅小鸟死亡
		this.player.OnDeath += Player_OnDeath;
		//Score
		this.player.OnScore = OnPlayerScore;
		//music playing
		this.musicManager.PlayBackgroundMusic();
	}

	private void Update()
	{
		clickMusicWhileFly();
	}
	private void Player_OnDeath()
	{	
		//更新最高分数
		if (this.score > this.scoreBest)
		{
			this.scoreBest = this.score;
			this.uiScoreBest.text = this.scoreBest.ToString();
		}
		//如果小鸟死亡，更新游戏状态为Game Over并关闭管道
		this.Status = GAME_STATUS.GameOver;
		this.pipelineManager.stop();
		//死亡音效
		this.musicManager.PlayDeathSound();
		//小鸟死亡状态
		this.player.Dead();
		}

	public void StartGame()
	{
		this.Status = GAME_STATUS.InGame;
		//this.player.rigidbodyBird.IsAwake();激活刚体组件,其实不用写这行代码，因为施加力时会自动激活
		player.Fly();
		pipelineManager.StartRun();
		Debug.LogFormat("StartGame : {0}", this.status);

	}

	public void UpdateUI()
	{
		this.PanelReady.SetActive(this.Status == GAME_STATUS.Ready);
		this.PanelInGame.SetActive(this.Status == GAME_STATUS.InGame);
		this.PanelGameOver.SetActive(this.Status == GAME_STATUS.GameOver);
	}
	
	public void ReStart()
	{
		this.Status = GAME_STATUS.Ready;
		this.pipelineManager.Init();
		this.player.Init();
		this.Scroe = 0;

	}

	void clickMusicWhileFly()
	{
		if (this.player.death && this.status != GAME_STATUS.InGame)
		{
			return;
		}
		//click音效
		if (Input.GetMouseButtonDown(0))
		{
			this.musicManager.PlayClickSound();
		}
	}
}
