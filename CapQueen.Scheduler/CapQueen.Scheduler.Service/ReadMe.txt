Quart.NET
参考： http://www.cnblogs.com/lzrabbit/archive/2012/04/13/2447609.html

1. Job添加：
实现IJob接口，实现void Execute(IJobExecutionContext context)，
注意所有的Log应该写成private static ILog _log = LogManager.GetLogger(typeof(T));
详见Demo CapQueen.Scheduler.HelloJob

关键点的是配置Job的Tragger。

所有的Job配置在CapQueen.Scheduler.Service/quartz_jobs.xml中，
   <schedule>
     <!--2.0版本中的job相当于1.x版本中的<job-detail>，这个节点是用来定义每个具体的任务的，多个任务请创建多个job节点即可-->
     <job>
       <!--任务名称，同一个group中多个job的name不能相同，若未设置group则所有未设置group的job为同一个分组（必须设置）-->
       <name>sampleJob</name>
       <!--任务所属分组，用于标识任务所属分组-->
       <group>sampleGroup</group>
       <!--工作任务的描述，用于描述任务具体内容-->
      <description>Sample job for Quartz Server</description>
       <!--任务类型，任务的具体类型及所属程序集，格式：实现了IJob接口的包含完整命名空间的类名,程序集名称-->
       <job-type>Quartz.Server.SampleJob, Quartz.Server</job-type>
       <!--<durable>（持久性）-如果一个Job是不持久的， 一旦没有触发器与之关联，它就会被从scheduler 中自动删除-->
      <durable>true</durable>
      <recover>false</recover>     
    </job>
     <!--trigger 任务触发器，用于定义使用何种方式触发任务(job)，同一个job可以定义多个trigger ，多个trigger 各自独立的执行调度，每个trigger 中必须且只能定义一种触发器类型(calendar-interval、simple、cron)
     calendar-interval 一种触发器类型，使用较少，此处略过-->
     <trigger>
       <!--简单任务的触发器，可以调度用于重复执行的任务-->
       <simple>
         <!--触发器名称，同一个分组中的名称必须不同-->
         <name>sampleSimpleTrigger</name>
         <!--触发器组-->
         <group>sampleSimpleGroup</group>
         <!--触发器描述-->
         <description>Simple trigger to simply fire sample job</description>
         <!--要调度的任务名称，该job-name必须和对应job节点中的name完全相同-->
         <job-name>sampleJob</job-name>
         <!--调度任务(job)所属分组，该值必须和job中的group完全相同-->
         <job-group>sampleGroup</job-group>
         <!--start-time(选填) 任务开始执行时间utc时间，北京时间需要+08:00，如：<start-time>2012-04-01T08:00:00+08:00</start-time>表示北京时间2012年4月1日上午8:00开始执行，注意服务启动或重启时都会检测此属性，若没有设置此属性或者start-time设置的时间比当前时间较早，则服务启动后会立即执行一次调度，若设置的时间比当前时间晚，服务会等到设置时间相同后才会第一次执行任务，一般若无特殊需要请不要设置此属性-->
         <misfire-instruction>SmartPolicy</misfire-instruction>
         <!--任务执行次数，如:<repeat-count>-1</repeat-count>表示无限次执行-->
         <repeat-count>-1</repeat-count>
         <!--任务触发间隔(毫秒)-->
         <repeat-interval>10000</repeat-interval>
        </simple>    
    </trigger>   
     <trigger>
       <!--cron复杂任务触发器使用cron表达式定制任务调度-->
       <cron>
          <name>sampleSimpleTrigger2</name>
          <group>sampleSimpleTrigger2</group>
          <job-name>sampleJob2</job-name>
          <job-group>sampleGroup2</job-group>
         <!--start-time(选填) 任务开始执行时间utc时间，北京时间需要+08:00，如：<start-time>2012-04-01T08:00:00+08:00</start-time>表示北京时间2012年4月1日上午8:00开始执行，注意服务启动或重启时都会检测此属性，若没有设置此属性，服务会根据cron-expression的设置执行任务调度；若start-time设置的时间比当前时间较早，则服务启动后会忽略掉cron-expression设置，立即执行一次调度，之后再根据cron-expression执行任务调度；若设置的时间比当前时间晚，则服务会在到达设置时间相同后才会应用cron-expression，根据规则执行任务调度，一般若无特殊需要请不要设置此属性-->
         <!--cron表达式-->
         <cron-expression>0/10 * * * * ?</cron-expression>
        </cron>  
    </trigger> 

2. Host
Quart.NET的Host方式比较方便，可以采用web/console/windows service
示例中采用了Topshelf扩展了console，使console直接具备exehost和servicehost两种能力。
exe Host： CMD中直接运行exe
Service Host: xxx.exe install[uninstall]
Web Host:demo 中没有实现，只要在global中appstart/end事件中启动scheduler.Start()/scheduler.Shutdown(true);

3.管理工具
添加Web管理控制台：/CrystalQuartzPanel.axd
注意在此管理控制台中，千万不要shutdown