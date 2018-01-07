using ARPGDemo.Character;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using UnityEngine.AI;
using ARPGDemo.Skill;

namespace AI.FSM
{
	///<summary>
	/// 状态机
	///</summary>
    [RequireComponent(typeof(CharactorSkillSystem),typeof(NavMeshAgent))]
	public class FSMBase : MonoBehaviour
	{
        #region Unity API
        private void Start()
        {
            ConfigFSM();
            InitComponent();
            InitDefaultState();
        }

        public void Update()
        {
            //执行当前状态行为
            currentState.Action(this);
            //检测当前状态中条件
            currentState.Reason(this);

            //查找目标
            SearchTarget();
        }
        #endregion

        #region 状态机成员
        //状态列表
        private List<FSMState> stateList;
        //当前状态
        private FSMState currentState;
        [Tooltip("默认状态编号")]
        public FSMStateID defaultStateID;//在编译器中配置 
        //默认状态
        private FSMState defaultState;
          
        //配置状态机
        //private void ConfigFSM()
        //{
        //    stateList = new List<FSMState>();
        //    //添加状态
        //    DeadState dead = new DeadState();
        //    stateList.Add(dead);

        //    IdleState idle = new IdleState();
        //    stateList.Add(idle);

        //    PursuitState pursuit = new PursuitState();
        //    stateList.Add(pursuit);

        //    AttackingState attacking = new AttackingState();
        //    stateList.Add(attacking);

        //    PatrollingState patrolling = new PatrollingState();
        //    stateList.Add(patrolling);

        //    //添加条件
        //    idle.AddMap(FSMTriggerID.NoHealth, FSMStateID.Dead);
        //    idle.AddMap(FSMTriggerID.SawTarget, FSMStateID.Pursuit);

        //    pursuit.AddMap(FSMTriggerID.NoHealth, FSMStateID.Dead);
        //    pursuit.AddMap(FSMTriggerID.ReachTarget, FSMStateID.Attacking);
        //    pursuit.AddMap(FSMTriggerID.LoseTarget, FSMStateID.Default);

        //    attacking.AddMap(FSMTriggerID.NoHealth, FSMStateID.Dead);
        //    attacking.AddMap(FSMTriggerID.WithoutAttackRange, FSMStateID.Pursuit);
        //    attacking.AddMap(FSMTriggerID.KilledTarget, FSMStateID.Default);

        //    patrolling.AddMap(FSMTriggerID.NoHealth, FSMStateID.Dead);
        //    patrolling.AddMap(FSMTriggerID.SawTarget, FSMStateID.Pursuit);
        //    patrolling.AddMap(FSMTriggerID.CompletePatrol, FSMStateID.Idle);
        //}

        private void ConfigFSM()
        {
            stateList = new List<FSMState>();
            var map = AIConfigurationReader.Load(aiFile);
            foreach (var item in map.Keys)
            {
                string className = "AI.FSM." + item + "State";
                var fsmStatus = Activator.CreateInstance(Type.GetType(className)) as FSMState;
                stateList.Add(fsmStatus);
                foreach (var key in map[item].Keys)
                {
                    FSMTriggerID tempTriggerID = (FSMTriggerID)Enum.Parse( typeof(FSMTriggerID),key);
                    FSMStateID tempStateID = (FSMStateID)Enum.Parse(typeof(FSMStateID), map[item][key]);
                    fsmStatus.AddMap(tempTriggerID,tempStateID);
                }
            }
        }

        /// <summary>
        /// 初始化默认状态
        /// </summary>
        private void InitDefaultState()
        {
            defaultState = stateList.Find(s => s.StateID == defaultStateID);
            if (defaultState == null) return;
            currentState = defaultState;
            currentState.EnterState(this);
        }
         
        /// <summary>
        /// 切换状态
        /// </summary>
        /// <param name="nextStateID"></param>
        public void ChangeState(FSMStateID nextStateID)
        {
            //计算需要切换的目标状态
            //如果需要切换的是默认状态，则直接返回；否则在状态列表中查找。
            var nextState = nextStateID == FSMStateID.Default ? defaultState   :  stateList.Find(s => s.StateID == nextStateID);

            currentState.ExitState(this);
            currentState = nextState;
            currentState.EnterState(this);
        }
        #endregion

        #region 为条件、状态提供的成员
        private void InitComponent()
        {
            chAnim = GetComponentInChildren<Animator>();
            chStatus = GetComponent<CharacterStatus>();
            navMeshAgent = GetComponent<NavMeshAgent>();
            skillSystem = GetComponent<CharactorSkillSystem>();
        }
        [HideInInspector]
        public Animator chAnim;
        [HideInInspector]
        public CharacterStatus chStatus;
        [HideInInspector]
        public Transform targetTF;
        [Tooltip("视野距离")]
        public float sightDistance = 10;
        [Tooltip("目标标签")]
        public string[] targetTag = new string[] { "Player" };
        [HideInInspector]
        public NavMeshAgent navMeshAgent;
        [Tooltip("移动速度")]
        public float moveSpeed = 2;
        [Tooltip("旋转速度")]
        public float rotateSpeed = 60;
        [HideInInspector]
        public CharactorSkillSystem skillSystem;
        [Tooltip("巡逻模式")]
        public PatrolMode patrolMode;
        [Tooltip("路点")]
        public Transform[] wapPoints;
        [Tooltip("寻路速度")]
        public float patrolSpeed = 1;
        [Tooltip("巡逻到达距离")]
        public float patrolArrivalDistance = 0.1f;
        [HideInInspector]
        public bool isPatrolComplete;

        public string aiFile;

        /// <summary>
        /// 查找目标
        /// </summary>
        private void SearchTarget()
        {
            var allObject = transform.CalculateAroundObject(sightDistance, 360, targetTag);
            if (allObject!=null && allObject.Length > 0)
            {
                allObject = allObject.FindAll(t => t.GetComponent<CharacterStatus>().HP > 0);
                if (allObject.Length>0)
                {
                    targetTF = allObject.GetMin(t => Vector3.Distance(t.position, transform.position));
                }
                else
                {
                    targetTF = null;
                }
            }
            else
                targetTF = null;
        }

        /// <summary>
        /// 移动到目标位置
        /// </summary>
        /// <param name="position">目标位置</param>
        /// <param name="moveSpeed">移动速度</param>
        /// <param name="stopDistance">停止的距离</param>
        public void MoveToTarget(Vector3 position,float moveSpeed,float stopDistance)
        {
            navMeshAgent.SetDestination(position);
            navMeshAgent.speed = moveSpeed;
            navMeshAgent.stoppingDistance = stopDistance;
            transform.LookPosition(position, rotateSpeed);
        }

        /// <summary>
        /// 停止移动
        /// </summary>
        public void StopMove()
        {
            //停止移动
            navMeshAgent.enabled = false;
            navMeshAgent.enabled = true;
        }
        #endregion
    }
}