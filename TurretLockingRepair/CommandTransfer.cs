using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using NLog;
using Sandbox.Game.Entities;
using Sandbox.Game.Weapons;
using Sandbox.ModAPI.Ingame;
using Torch;
using Torch.API;
using VRage.Groups;

namespace TurretLockingRepair
{
    public class Plugin : TorchPluginBase {
        
        private long UpdateCount = 0; //update计时器
  
        public override void Update()
        {
            base.Update();
            UpdateCount++;

            if (UpdateCount % 1000 == 0)
            {
                FlushTurrets();
            }
        }


        public static void FlushTurrets()
        {
          
            foreach (var gGroups in MyCubeGridGroups.Static.Physical.Groups)
            {
                //获取物理组的所有节点
                foreach (var groupsNode in gGroups.Nodes)
                {
                    foreach (var myLargeTurretBase in groupsNode.NodeData.GetFatBlocks<MyLargeTurretBase>())
                    {
                        if (myLargeTurretBase.IsShooting)
                        {
                            myLargeTurretBase.ApplyAction("Shoot_Off");
                            myLargeTurretBase.ApplyAction("Shoot_On");
                        }
                        else
                        {
                            myLargeTurretBase.ApplyAction("Shoot_On");
                            myLargeTurretBase.ApplyAction("Shoot_Off");
                        }
                    }
                }
            }
    
            
       
        }
       
        
        
    }
}