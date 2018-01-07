using System.Collections.Generic;
using UnityEngine; 
using System;
using wzx;

namespace AI.FSM
{
    /// <summary>
    /// 状态机
    /// </summary>
    ///  
    [RequireComponent(typeof(wzx.Gun))]
    public class BaseFSM : MonoBehaviour
    {
        #region Unity API 

        private void Awake()
        {
            ConfigFSM();

            InitComponent();

            InitDefaultState();
        } 

        private void Update()
        { 
            currentState.Reason(this);
            currentState.Action(this);
        }
        #endregion
         
        #region 状态机数据
        /// <summary>当前状态</summary>
        [SerializeField]
        public FSMState currentState;
        /// <summary>默认状态</summary>
        private FSMState defaultState;
        public FSMStateID defaultStateId;
        /// <summary>状态库</summary>
        private List<FSMState> states = new List<FSMState>();
        public string[] targetTags = { "Player" };
        /// <summary>角色状态组件</summary>
        [HideInInspector]
        public EnemyStatus enemyStatus;
        [HideInInspector]
        public Animator anim;
        #endregion

        #region 状态机行为
        private void ConfigFSM()
        {
            var dic = AIConfigurationReader.Load(aiConfigFile);
            //创建状态对象
            foreach (var stateName in dic.Keys)
            {
                var type = Type.GetType("AI.FSM." + stateName + "State");
                var stateObj = Activator.CreateInstance(type) as FSMState;
                //增加映射
                foreach (var triggerId in dic[stateName].Keys)
                {
                    var trigger = (FSMTriggerID)(Enum.Parse(typeof(FSMTriggerID), triggerId));
                    var state = (FSMStateID)(Enum.Parse(typeof(FSMStateID), dic[stateName][triggerId]));
                    stateObj.AddMap(trigger, state);
                }
                //放入状态库 
                states.Add(stateObj);
            }
        }
          
        private void InitComponent()
        {
            enemyStatus = GetComponent<EnemyStatus>();
            anim = GetComponentInChildren<Animator>();
            playerTF = PlayerStatus.Instance.transform;
        }

        //初始化默认状态
        private void InitDefaultState()
        {
            //用默认状态编号为其他三个状态数据赋值
            defaultState = states.Find(p => p.stateid == defaultStateId);
            currentState = defaultState;
            currentState.EnterState(this);//进入当前状态 
        } 
         
        /// <summary>切换状态</summary>
        public void ChangActiveState(FSMStateID nextStateId)
        {
            //查找状态对象
            FSMState nextState = nextStateId == FSMStateID.Default ? defaultState : states.Find(p => p.stateid == nextStateId);
            
            //当前状态 --- 出
            currentState.ExitState(this);
            //做出下步的安排
            currentState = nextState;
            //下一个状态--- 进
            currentState.EnterState(this);
        }
         
        #endregion

        #region 为状态和条件提供的数据
        /// <summary>
        /// 跑步速度
        /// </summary>
        public float runSpeed = 5;
        /// <summary>
        /// 巡逻到达的距离
        /// </summary>
        public float patrolArrivalDistance = 1;
        [HideInInspector]
        /// <summary>
        /// 路线
        /// </summary>
        public Vector3[] wayPath;
        /// <summary>
        /// 旋转速度
        /// </summary>
        public float rotateSpeed = 10;
        [HideInInspector]
        /// <summary>
        /// 是否完成巡逻
        /// </summary>
        public bool IsPatrolComplete = false;
        /// <summary>
        /// AI配置文件
        /// </summary>
        public string aiConfigFile = "AI_01.txt";
        public float attackInterval = 5;
        [HideInInspector]
        public Transform playerTF; 
        #endregion
    }
}
