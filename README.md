# Element Plus Plugin 

###活字格官网（https://www.grapecity.com.cn/solutions/huozige）

### 快速开始 
（插件的使用帮助说明在第二部分，请下拉查看）
#### 编译
- 依赖 typescript 4.4.4, 请确保电脑安装有 4x版本以上的SDK。
- 插件会在 设计器的默认安装路径下 获取引用,如果你的设计器是自定义的安装路径，需要更新引用的路径，具体可阅读 [活字格-插件开发-插件开发流程](https://help.grapecity.com.cn/pages/viewpage.action?pageId=56534757)。

#### 分支管理
- master 是发布的最新版本,和[插件商城](https://marketplace.grapecity.com.cn/ApplicationDetails?productID=SP2203310001&productDetailID=D2203310006&tabName=Tabs_detail) 最新版本对应。 
- dev 开发人员会在该分支上开发，你可以在此分支获取最新的代码，仅限学习和测试，请勿在生产环境中使用。 


### 参与贡献

1.  Fork 本仓库
2.  新建 Feat_xxx 分支
3.  提交代码
4.  新建 Pull Request

***

## 目录

*   [说明](#说明)

*   [Element UI 单元格类型](#element-ui-单元格类型)

    *   *   [EL-日历](#el-日历)

        *   [EL-头像](#el-头像)

        *   [EL-面包屑](#el-面包屑)

        *   [EL-级联选择器](#el-级联选择器)

        *   [EL-日期选择器](#el-日期选择器)

        *   [EL-输入框](#el-输入框)

        *   [EL-计数器](#el-计数器)

        *   [EL-导航菜单](#el-导航菜单)

        *   [EL-分页](#el-分页)

        *   [EL-进度条](#el-进度条)

        *   [EL-评分 ](#el-评分-)

        *   [EL-选择器](#el-选择器)

        *   [EL-滑块](#el-滑块)

        *   [EL-步骤条](#el-步骤条)

        *   [EL-标签页头](#el-标签页头)

        *   [EL-标签](#el-标签)

        *   [EL-时间线](#el-时间线)

        *   [EL-时间选择器](#el-时间选择器)

        *   [EL-穿梭框](#el-穿梭框)

        *   [EL-文件上传](#el-文件上传)

        *   [EL-表格](#el-表格)

*   [Element UI 命令](#element-ui-命令)

    *   *   [EL-通知](#el-通知)

        *   [EL-消息](#el-消息)

        *   [EL-弹框](#el-弹框)

*   [Element UI自定义](#element-ui自定义)

    *   *   [自定义EL-回到顶部](#自定义el-回到顶部)

        *   [自定义EL-日历](#自定义el-日历)

        *   [自定义EL-级联选择器](#自定义el-级联选择器)

        *   [自定义EL-日期选择器](#自定义el-日期选择器)

        *   [自定义EL-导航菜单](#自定义el-导航菜单)

        *   [自定义EL-选择器](#自定义el-选择器)

        *   [自定义EL-表格](#自定义el-表格)


# Element UI插件说明

## 说明

Element UI是一个基于vue的一个ui框架,该框架基于vue提供了很多美观易用的组件供我们开发页面使用。

***

***

## Element UI 单元格类型

#### EL-日历

**使用场景**

*   可以直接创建一个日历

**使用方式**

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_vA4H44Xjbq.png)

**设计时**

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_TQCMJddTPC.png)

**运行时**

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_7JRZWq_DK7.png)

**单元格设置**

| 名称             | 说明                      |
| -------------- | ----------------------- |
| 默认值            |                         |
| 日历项目命令-编辑值变更命令 | 日历选择更改时触发               |
| 日历项目命令-编辑双击命令  | 双击某一日时触发                |
| 日历日程命令-编辑单击命令  | 单击日程触发的命令               |
| 日历日程命令-编辑双击命令  | 双击日程触发的命令               |
| 配置日程           | 绑定日程数据源，绑定表列为日程的值、时间、文本 |
| 周起始日           | 设置每周的起始日期               |

**备注**

当空间不足时，日程会展示为一个灰点

***

#### EL-头像

**使用场景**

*   可以替代使用图片单元格格式作为头像

*   优势在于：支持使用系统用户、支持找不到头像时显示用户名、支持角标

*   劣势在于：不支持将单元格类型添加到列表视图、不支持样式模板

**使用方式**

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_RxgYW84wEm.png)

**设计时**

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_F66SJuq9tS.png)

**运行时**

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image__wLi0zwRK5.png)

**单元格设置**

| 名称       | 说明                                                                                                                                       |
| -------- | ---------------------------------------------------------------------------------------------------------------------------------------- |
| 编辑命令     | 点击单元格时执行的命令                                                                                                                              |
| 默认头像     |                                                                                                                                          |
| 头像形状     | 圆形\|正方形                                                                                                                                  |
| 显示模式     | 填充\|包含\|覆盖\|原始尺寸\|缩小                                                                                                                     |
| 通知角标     | 角标大小是固定的，而不是按单元格大小缩放                                                                                                                     |
| 显示系统用户头像 | 勾选：单元格值为当前用户的系统用户名。如值为张三则显示张三的头像，如果用户没有设置头像，单元格会显示用户名，通常配合用户信息视图显示用户列表的头像&#xA;如需使用当前用户的头像，可以使用%CurrentUser%关键字作为单元格的值。&#xA;不勾选：单元格值为图像路径 |

不勾选

***

#### EL-面包屑

**使用场景**

*   显示当前页面的位置，便于浏览返回

**使用方式**

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_ZkbsQBWpQV.png)

*   初始化面包屑单元格值的三种方法

1.  使用静态值

    a.将面包屑单元格放置到每个页面，在设计时设置默认值

2.  使用数据库配置

    a.将面包屑单元格放入母版页

    b.创建一个表作为“站点”地图，如下:

    | 页面名称 | 页面路径       |
    | ---- | ---------- |
    | 用户   | 主页/用户管理/用户 |
    | 角色   | 主页/用户管理/角色 |
    | 组织   | 主页/用户管理/组织 |
    | 导入   | 主页/数据/导入   |
    | ...  |            |

    c.在页面加载命令中，通过设置变量命令查询当前页面的名称，进一步查询页面路径，然后通过设置单元格属性命令设置面包屑的值

3.  使用菜单单元格类型配置

    a.设置一个单元格为菜单类型

    b.在页面加载命令中，从菜单中获取页面路径，然后更改面包屑的值为该路径

**设计时**

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_o-RcTeY-Js.png)

**运行时**

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_e4nZlJLoMQ.png)

**单元格设置**

| 名称     | 说明          |
| ------ | ----------- |
| 编辑点击命令 | 点击面包屑时将执行命令 |
| 默认值    |             |
| 分隔符    |             |

***

#### EL-级联选择器

**使用场景**

*   如果选项具有清晰的层次结构，则可以使用 级联选择器来查看和选择。

**使用方式**

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_l7Q7AJmL79.png)

**设计时**

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_m2VqEZZH7p.png)

**运行时**

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_EZK2iw-8K9.png)

**单元格设置**

| 名称              | 说明                                                                                                                                      |
| --------------- | --------------------------------------------------------------------------------------------------------------------------------------- |
| 编辑命令            | 值更改时将执行命令                                                                                                                               |
| 数据验证            | 配置单元格数据验证，仅在更新命令执行或请求服务器命令执行时验证                                                                                                         |
| 单元格权限           | 基于用户角色控制可见/可用权限                                                                                                                         |
| 默认值             |                                                                                                                                         |
| 选项来自数据库         | 设置选项是否与数据库绑定                                                                                                                            |
| 配置选项            | 设置非绑定数据库时的选项                                                                                                                            |
| 绑定数据源           | 勾选选项来自数据库后，设置具体的绑定                                                                                                                      |
| 子菜单展开方式         | 展示下一级菜单的方式，点击/鼠标悬停                                                                                                                      |
| 占位文本            | 未选择时的默认显示                                                                                                                               |
| 选项分隔符           | 设置不同级别值间得到分割符                                                                                                                           |
| 多选              | 是否允许多选                                                                                                                                  |
| 多选下折叠标签         | 必须多选启用。设置是否折叠更多的标签                                                                                                                      |
| 标签样式            | 必须多选启用。设置标签的样式，成功\|信息\|警告\|危险                                                                                                           |
| 允许选择任意一级选项      | 勾选后，取消父子节点关联，可以选择任意一级选项                                                                                                                 |
| 输入框中显示完整路径      | 是否展示标签的完整路径                                                                                                                             |
| 值返回完整路径         | 必须多选禁用。设置单元格的值时是否包含完整路径。                                                                                                                |
| 可搜索选项           | 设置是否允许在选择器内进行关键字搜索                                                                                                                      |
| 显示清空按钮          | 勾选时，当选择器内不为空，鼠标悬停时下拉按钮会变为清空按钮                                                                                                           |
| 禁用              |                                                                                                                                         |
| 可用操作            | 直接拖拽可用操作到页面上，自动生成操作单元格命令                                                                                                                |
| 可用操作-设置数据源(对象树) | 使用动态 JSON 对象树作为数据源，JSON 格式示例见备注。&#xA;示例中假设'值属性'为value，'标签属性名'为label，'下级属性名'为'children'。&#xA;通常，JSON数据源可以通过HTTP请求命令从Web服务获取，也可以通过服务端命令获取 |
| 可用操作-设置数据源(二维表) | 使用二维表JSON对象树作为数据源，JSON 格式示例见备注。&#xA;示例中假设‘值属性’为value，‘标签属性名’为label，‘父级值属性’为‘parentValue’。&#xA;二维表格式是数据库中常见的多级数据保存格式。                    |
| 可用操作-获取选中的节点    | 必须多选启用。&#xA;获取选中项目并将数组返回到变量&#xA;参数：只是叶子节点：勾选后，只返回选中的叶子节点                                                                                |
| 可用操作-重新加载绑定项目   | 必须绑定数据源启用。&#xA;如果改变数据绑定后，绑定项未自动重新加载数据，使用这个操作可以强制重新加载绑定项                                                                                 |

**备注**

*   对象树-JSON

```纯文本
[
    {
        "value": 1,
        "label": "Department1",
        "children": [
            {
                "value": 2,
                "label": "Sub-department1"
            },
            {
                "value": 3,
                "label": "Sub-department2",
                "children": [
                    {
                       "value": 4,
                       "label": "Sub-department2-1"
                    }
                ]
            }
        ]
    },
    {
        "value": 5,
        "label": "Department2"
    },
    {
        "value": 6,
        "label": "Department3"
    }
]
```

*   二维表-JSON

```纯文本
[
    {"value": 1, "label": "Department1", "parentValue": null},
    {"value": 2, "label": "Department1-1", "parentValue": 1},
    {"value": 3, "label": "Department1-2", "parentValue": 1},
    {"value": 4, "label": "Department1-2-1", "parentValue": 3},
    {"value": 5, "label": "Department2", "parentValue": null},
    {"value": 6, "label": "Department3", "parentValue": null}
]
```

***

#### EL-日期选择器

**优劣对比**

与活字格内置单元格类型-日期对比

优势：

*   更现代的UI界面

*   更丰富的类型，支持按年/月/日/周/日期时间/月份范围/日期范围/日期时间范围

*   更便于选择历史年份

*   支持头部图表

*   支持添加清空按钮

劣势

*   不支持添加进表格

*   不支持获得焦点时全选文本内容

*   不支持设置单元格样式

*   不支持使用单元格格式

*   不支持下拉按钮显示设置

**使用方式**

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_sh_-VSrDPj.png)

**设计时**

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_C6jS5NhGS_.png)

**运行时**

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_RNFZn6Exw2.png)

**单元格设置**

| 名称          | 说明                                                                                                                                                                                                                                                                    |
| ----------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| 编辑命令        |                                                                                                                                                                                                                                                                       |
| 数据验证        |                                                                                                                                                                                                                                                                       |
| 单元格权限       |                                                                                                                                                                                                                                                                       |
| 默认值         |                                                                                                                                                                                                                                                                       |
| 显示类型        | 设置不同的选择类型                                                                                                                                                                                                                                                             |
| 开始日期占位文本    | 必须是范围模式。设置开始日期占位文本                                                                                                                                                                                                                                                    |
| 范围分隔符       | 必须是范围模式。设置分隔符样式                                                                                                                                                                                                                                                       |
| 结束日期占位文本    | 必须是范围模式。设置结束日期占位文本                                                                                                                                                                                                                                                    |
| 占位文本        | 必须不是范围模式。                                                                                                                                                                                                                                                             |
| 显示在输入框中的格式  | 参考见：[https://element.eleme.io/#/en-US/component/date-picker](https://element.eleme.io/#/en-US/component/date-picker "https://element.eleme.io/#/en-US/component/date-picker")&#xA;可以使用\[]作为转义字符                                                                       |
| 周起始日        | 必须类型为日/周/日期时间/日期范围/日期时间范围时                                                                                                                                                                                                                                            |
| 头部图表        |                                                                                                                                                                                                                                                                       |
| 显示清空按钮      |                                                                                                                                                                                                                                                                       |
| 只读          |                                                                                                                                                                                                                                                                       |
| 禁用          |                                                                                                                                                                                                                                                                       |
| 可用操作-获取选择范围 | 必须是范围模式。此命令可以获取用户选择的起始值和结束值，并将其保留到数据库或者作为查询条件进行查询。&#xA;策略：&#xA;  · 如果类型是月份范围，当用户选择 2000/1 到 2000/3，结果开始值为 "2000/1/1 00:00:00" ，结果结束值为 "2000/3/31 23:59:59.999"&#xA;  · 如果类型是日期范围，当用户选择 2000/1/1 到 2000/3/5，结果开始值为 "2000/1/1 00:00:00" ，结果结束值为 "2000/3/5 23:59:59.999" |

***

#### EL-输入框

**优劣对比**

与活字格内置单元格类型-文本框对比

优势：

*   更现代的UI界面

*   支持设置最大输入长度

*   支持尾部图表

*   支持添加清空按钮

劣势

*   不支持添加进表格

*   不支持值唯一校验

*   不支持获得焦点时全选文本内容

*   不支持设置单元格样式

*   不支持使用单元格格式

**使用方式**

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_ztoGglX6Ga.png)

**设计时**

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_tUyNN0PP3L.png)

**运行时**

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_5ngsO2tbde.png)

**单元格设置**

| 名称            | 说明                        |
| ------------- | ------------------------- |
| 编辑命令          |                           |
| 数据验证          | 在数据表更新、请求服务端命令时验证         |
| 单元格权限         |                           |
| 默认值           |                           |
| 类型            | 文本框、多行文本框、密码框             |
| 最大长度          |                           |
| 最大长度-显示输入字数统计 | 必须最大长度已设置，且为文本框、多行文本框。    |
| 占位文本          |                           |
| 头部图标          |                           |
| 尾部图标          |                           |
| 只读            |                           |
| 禁用            |                           |
| 显示清空按钮        | 设置后，当内容非空时会出现             |
| 可用操作          | 直接拖拽可用操作到页面上，自动生成操作单元格命令  |
| 可用操作-设置焦点     | 调用操作单元格命令，使输入框获取键盘焦点      |
| 可用操作-选中文字     | 调用操作单元格命令，使输入框获取键盘焦点并全选文字 |

***

#### EL-计数器

**优劣对比**

与活字格内置单元格类型-数字对比

优势：

*   更现代的UI界面

*   支持设置最大/最小输入值

劣势

*   不支持添加进表格

*   不支持获得焦点时全选文本内容

*   不支持千分位

*   不支持设置单元格样式

*   不支持使用单元格格式

**使用方式**

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_p-hnNh98-5.png)

**设计时**

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_YQxR5J3ZU2.png)

**运行时**

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_4U0mv3RCMM.png)

**单元格设置**

| 名称        | 说明                       |
| --------- | ------------------------ |
| 编辑命令      |                          |
| 数据验证      |                          |
| 单元格权限     | 根据用户角色控制可见/可用            |
| 默认值       |                          |
| 最小值       |                          |
| 最大值       |                          |
| 步长        | 设置后，点击控制按钮会以步长值增长/减小     |
| 只能输入步长的倍数 | 勾选后，则无法输入非步长整数倍的值        |
| 小数位数      | 默认0                      |
| 占位为本      |                          |
| 使用控制按钮    | 设置是否显示控制按钮               |
| 控制按钮显示在右侧 | 设置控制按钮位置及样式              |
| 禁用        |                          |
| 可用操作      | 直接拖拽可用操作到页面上，自动生成操作单元格命令 |
| 设置最小值     | 调用操作单元格命令，设置最小值          |
| 设置最大值     | 调用操作单元格命令，设置最大值          |
| 设置步长      |                          |
| 设置焦点      |                          |

***

#### EL-导航菜单

**优劣对比**

与活字格内置单元格类型-菜单对比

优势：

*   更现代的UI界面

*   可以从数据库生成菜单项目

劣势：

*   不支持使用单元格样式

**使用方式**

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_UbbMKj3lNT.png)

**设计时**

垂直/水平

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_venkrWLali.png)

**运行时**

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_zI_CpoXzz1.png)

**单元格设置**

| 名称          | 说明                                                                                         |
| ----------- | ------------------------------------------------------------------------------------------ |
| 编辑选择命令      | 选择叶子节点时执行                                                                                  |
| 编辑点击命令      | 选择任意项目时执行                                                                                  |
| 从数据库生成菜单项目  | 勾选后，从数据库生成菜单                                                                               |
| 配置菜单项       | 不勾选从数据库生成菜单项目时进行菜单配置。同内置菜单类型                                                               |
| 绑定数据源       | 勾选从数据库生成菜单项目。设置选择项、查询条件、查询行数、排序等等                                                          |
| 布局          | 垂直/水平                                                                                      |
| 水平折叠收起菜单    | 必须垂直布局                                                                                     |
| 开启折叠动画      | 必须垂直布局。设置是否展示折叠动画                                                                          |
| 只保持一个子菜单的展开 | 必须垂直布局                                                                                     |
| 子菜单打开的触发方式  | 必须水平布局。设置鼠标悬停/点击时打开子菜单                                                                     |
| 主题          | 浅色/深色/自定义                                                                                  |
| 可用操作        | 直接拖拽可用操作到页面上，自动生成操作单元格命令                                                                   |
| 获取选择项路径     | 选择路径是一个数组，例如\[Home/Categroy/xxxPage]。可以使用TEXTJOIN("/",1,valuePath)将其转换为字符串，或者通过循环命令访问每一级路径 |
| 设置折叠        |                                                                                            |
| 重新加载绑定项目    | 必须勾选从数据库生成菜单项目。如果切换数据源后数据没有自动加载，点击强制重新加载绑定项目。                                              |
| 隐藏菜单项       |                                                                                            |
| 设置徽标        |                                                                                            |

***

#### EL-分页

**优劣对比**

与活字格内置单元格类型-分页导航按钮对比

优势：

*   更现代的UI界面

*   弹性布局设置

*   可展示总项数

*   允许用户在运行中更改每页展示行数

*   可设置只有一页时自动隐藏

**使用方式**

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_45vBilvvaP.png)

**设计时**

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_VyFnBJCw2Q.png)

**运行时**

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_lhYcCVTy-6.png)

**单元格设置**

| 名称               | 说明                                       |
| ---------------- | ---------------------------------------- |
| 分页变更命令           | 当页面索引、页面展示行数由用户通过界面操作变更时调用命令                        |
| 选择Element表格      | 控制选择的数据源                                 |
| 选择表格/选择Element表格 | 根据选择Element表格的勾选情况，展示不同的名称，绑定目标类型的数据源    |
| 每页显示行数           | 设置一页显示多少记录，总页数会自动同步                      |
| 最大页码按钮显示数        | 设置同时最多显示的页码按钮树                           |
| 控件布局             | 设置控件元素，调整每页显示行数/上一页/页号/下一页/直接前往/空白间距/总行数 |
| 调整每页显示行数配置       | 必须调整每页显示行数开启。设置前端手动调整每页行数时的选项            |
| 上一页文本            | 设置后，用文本替换左箭头作为上一页按钮                      |
| 下一页文本            | 设置后，用文本替换右箭头作为下一页按钮                      |
| 使用小型分页样式         |                                          |
| 为分页按钮添加背景色       |                                          |
| 只有一页时隐藏          | 只有一页时隐藏分页单元格                             |
| 禁用               |                                          |
| 可用操作             | 直接拖拽可用操作到页面上，自动生成操作单元格命令                 |
| 设置当前每页显示行数       |                                          |
| 设置当前页码           |                                          |
| 设置总行数            | 当未绑定数据源时使用                               |
| 执行命令             |                                          |

***

#### EL-进度条

**使用场景**

*   用户想要使用进度条展示数字

**使用方式**

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_2I8Orv_cyK.png)

**设计时**

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_sJWsMp4ygH.png)

**单元格设置**

| 名称             | 说明                                                                              |
| -------------- | ------------------------------------------------------------------------------- |
| 编辑值变更命令        | 当进度条值变更时执行命令                                                                    |
| 显示类型           | 线性/环形/仪表盘型                                                                      |
| 进度条的宽度         | 默认为6，单位固定为像素，控制进度条的宽度                                                           |
| 进度条当前状态        | 空/成功/异常/警告，只有空时定义的显示文本会生效，其他状态显示为对应图标                                           |
| 颜色             | 必须进度条状态为空。设置生效进度的颜色                                                             |
| 显示进度条文字内容      |                                                                                 |
| 进度条显示文字内置在进度条内 | 必须进度条显示类型为线性。勾选后，进度条文字显示在进度条内                                                   |
| 自定义显示文本        | 必须进度条状态为空生效。设置显示文本，默认为空，为空时显示百分比文本。用户使用公式，例如‘=“共使用了”\&A1&“G空间”’，显示为：“共使用了30G空间” |
| 可用操作           | 直接拖拽可用操作到页面上，自动生成操作单元格命令                                                        |
| 可用操作-更改背景颜色    |                                                                                 |
| 可用操作-更改当前状态    |                                                                                 |

***

#### EL-评分&#x20;

**使用场景**

*   需要打分时

**使用方式**

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_lPUd3QjvYv.png)

**设计时**

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_9OQQrL4znF.png)

**运行时**

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_suFI7l_9Aq.png)

**单元格设置**

| 名称          | 说明                                                                                  |
| ----------- | ----------------------------------------------------------------------------------- |
| 编辑命令        |                                                                                     |
| 最大分值        |                                                                                     |
| 低分和中等分数的界限值 | 设置低分和中分的界限，低分默认为深灰色，中等分数默认为黄色                                                       |
| 高分和中等分数的界限值 | 设置中分和高分的界限，高分数默认为深黄黄色                                                               |
| 显示内容        | 空/辅助文字/分数（自动显示当前星数）                                                                 |
| 配置辅助文字      | 必须显示内容设置为辅助文字。设置文字列表，自动对应当前星数，第一项对应1星。文字列表数量少于最大分值时，高分显示为空；文字列表数量大于最大分值时，多余文字不会被对应。 |
| 未选中图标的颜色    |                                                                                     |
| 选中图标的颜色     | 设置0-3项，依次对应低、中、高分的颜色，高分没有对应会沿用低一档的颜色                                                |
| 只读          |                                                                                     |
| 只读时未选中图标的颜色 |                                                                                     |
| 允许半选        | 允许选择半星                                                                              |

***

#### EL-选择器

**优劣对比**

与活字格内置单元格类型-组合框按钮对比

优势：

*   更现代的UI界面

*   支持服务端搜索（详细解释见单元格设置-服务端搜索）

*   移动端也支持输入

*   支持服务端搜索结果缓存（详细解释见单元格设置-绑定数据源）

劣势：

*   不支持添加进表格

*   不支持获得焦点时全选文本内容

*   不支持设置单元格样式

*   不支持使用单元格格式

*   不支持设置下拉列数

*   不支持控制下拉按钮

*   不支持设置下拉框宽度

*   不支持设置最大下拉个数

**使用方式**

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_Idccb2y2Er.png)

**设计时**

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image__58GdtDbOb.png)

**运行时**

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_2Z_XM5x9-H.png)

**单元格设置**

| 名称                | 说明                                                                                                                                                                                                                             |
| ----------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| 编辑命令              | 值变动时执行命令                                                                                                                                                                                                                       |
| 数据验证              |                                                                                                                                                                                                                                |
| 单元格权限             |                                                                                                                                                                                                                                |
| 默认值               |                                                                                                                                                                                                                                |
| 从数据库生成选项          | 设置数据源是否绑定数据库。若绑定数据库，选择器在生成选项时会自动去重                                                                                                                                                                                             |
| 配置选项              | 未绑定数据库时设置选项                                                                                                                                                                                                                    |
| 绑定数据源             | 必须勾选从数据库生成选项。除了选择项、查询条件、行数等类似数据表查询的设置外，还有一个其他设置，可以设置开启缓存并设置缓存过期时间。此处缓存是指查询结果会放在服务器的内存中，直到过期，在高并发场景下，相同查询不会反复访问数据库，可以大幅降低数据库压力。&#xA;此外，保留的缓存不可以设置权限过滤，所以开启缓存后，为不同用户设置的不同查询结果是不会生效的。如需权限控制查询内容，请不要开启此功能。                         |
| 添加空项              | 必须绑定数据库生成选项。                                                                                                                                                                                                                   |
| 占位文本              |                                                                                                                                                                                                                                |
| 选项为空时显示的文本        |                                                                                                                                                                                                                                |
| 多选                | 是否允许多选                                                                                                                                                                                                                         |
| 多选模式下折叠Tag        | 必须允许多选。                                                                                                                                                                                                                        |
| 多选时最多选择的项目数       | 必须允许多选。                                                                                                                                                                                                                        |
| 可搜索选项             | 设置用户是否可以在输入框中输入关键字搜索项目                                                                                                                                                                                                         |
| 服务端搜索             | 必须绑定数据库生成选项。服务端搜索是针对组合框的性能提升。使用内置单元格类型组合框时，如果进行了搜索，组合框会先获取对应绑定数据，再在页面内进行搜索计算。使用EL-选择器并启用了服务端搜索时，进行搜索，选择器会将选项传给服务端，由服务端进行数据筛选后，仅回传搜索结果。对于搜索选项数量较大时，性能会有明显提升。                                                                    |
| 保留搜索关键字           | 必须允许多选。必须绑定数据库生成选项。在输入搜索条件后，选择一项后是否清空输入的搜索条件。                                                                                                                                                                                  |
| 允许用户创建新条目         | 允许用户输入值创造不存在的项并选择                                                                                                                                                                                                              |
| 搜索结果为空时显示文本       |                                                                                                                                                                                                                                |
| 显示清空按钮            |                                                                                                                                                                                                                                |
| 禁用                |                                                                                                                                                                                                                                |
| 可用操作              | 直接拖拽可用操作到页面上，自动生成操作单元格命令                                                                                                                                                                                                       |
| 可用操作-设置数据源（字符串数组） | 使用动态 JSON 字符串数组作为单元格的数据源，例如：&#xA;\[&#xA;"香蕉",&#xA;"苹果",&#xA;"鸭梨"&#xA;]&#xA;通常，数据源可以通过HTTP请求命令从Web服务获取，也可以通过服务端命令获取                                                                                                             |
| 可用操作-设置数据源（对象数组）  | 使用动态 JSON 字符串数组作为单元格的数据源，例如：&#xA;\[&#xA;{"value": 1, "label": "香蕉"},&#xA;{"value": 2, "label": "苹果"},&#xA;{"value": 3, "label": "鸭梨"}&#xA;]&#xA;以上数据假设‘值属性’为value，‘显示文本’为label&#xA;通常，JSON数据源可以通过HTTP请求命令从Web服务获取，也可以通过服务端命令获取 |
| 设置焦点              |                                                                                                                                                                                                                                |
| 获取显示文本            |                                                                                                                                                                                                                                |
| 重新加载绑定项目          | 必须绑定数据库生成选项。                                                                                                                                                                                                                   |

**限制**

*   EL-选择器不支持虚拟 DOM，这意味着下拉菜单中的每个项目都会创建至少一个 DOM。 如果下拉列表中有数千个项目时，性能会很差。 这种情况下，最好是使用绑定模式，并启用“可搜索选项”和“服务端搜索”

*   以下情况下，绑定数据库源后查询行数会返回异常

    1.  使用从数据库生成选项，但数据源中有重复数据

    2.  设置查询行数

    3.  返回结果可能小于查询行数的设定值。因为选择器会自动去重，且这步操作在数据源查询后，所以当数据源查询的结果行中有重复数据时，选项会变少。（例如：数据库查询结果为：1,1,2,2,3,...，但设定了查询行数为3，则返回1,1,2给EL-选择器作为选项，此时选择器自动去重，导致最后选项只有2项为1和2）

***

#### EL-滑块

**使用场景**

*   创建一个在固定范围内的自由滑动的滑块

**使用方式**

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_rh3QYQCSGj.png)

**设计时**

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_FMHBrr--Pt.png)

**运行时**

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_QNMs8fhg1A.png)

**单元格设置**

| 名称         | 说明                                               |
| ---------- | ------------------------------------------------ |
| 编辑命令       | 值变动时触发命令                                         |
| 单元格权限      |                                                  |
| 配置标记       |                                                  |
| 默认值        |                                                  |
| 最小值        |                                                  |
| 最大值        |                                                  |
| 步长         |                                                  |
| 布局         | 横向/竖向                                            |
| 范围选择       | 启用后有两个滑块，可以选择一个范围                                |
| 显示输入框      | 可以输入值控制滑块                                        |
| 显示输入框的控制按钮 | 生成类似计数器的                                         |
| 显示间断点      | 显示滑块的停止点。注意，每个可用值都会生成一个停止点。需要调整最大值、最小值、步长，以确保效果。 |
| 显示提示信息     | 当拖动及悬停时显示自定义信息                                   |
| 自定义提示信息    | 自定义显示内容。可用参数滑块值                                  |
| 禁用         |                                                  |
| 可用操作       | 直接拖拽可用操作到页面上，自动生成操作单元格命令                         |
| 设置最小值      |                                                  |
| 设置最大值      |                                                  |
| 设置标记       |                                                  |
| 获取选择范围     | 必须勾选范围选择                                         |

***

#### EL-步骤条

**使用场景**

*   引导用户按照流程完成任务

*   步骤可根据实际应用场景设置

**使用方式**

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_Icsd1Jqk6k.png)

**设计时**

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_QTk_nHeX2d.png)

**运行时**

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_zxX04T5ybg.png)

**单元格设置**

| 名称             | 说明                                                             |
| -------------- | -------------------------------------------------------------- |
| 编辑值变更命令        | 步骤变更时触发命令                                                      |
| 选项来自数据库        |                                                                |
| 配置步骤项目         |                                                                |
| 绑定数据源          | 必须选项来自数据库。设置选择项、查询条件、查询行数、排序、缓存                                |
| 当前步骤的状态        | 表示当前步骤的状态，不同状态有不同的样式，共有5种：等待、过程、完成、错误、成功                       |
| 结束步骤的状态        | 表示结束（已完成）步骤的状态，状态及样式同当前步骤状态的设置。&#xA;此外，如果已完成步骤状态是错误，进程状态将始终为等待 |
| 布局             | 横向/纵向                                                          |
| 简洁风格           |                                                                |
| 居中对齐           |                                                                |
| 可用操作           | 直接拖拽可用操作到页面上，自动生成操作单元格命令                                       |
| 可用操作-设置当前步骤的状态 |                                                                |

***

#### EL-标签页头

**使用场景**

*   样式更为现代的标签页头

**使用方式**

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_P4FqP-6Oe-.png)

**设计时**

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_zkZ2e8V6X5.png)

**运行时**

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_UedNjqKv0O.png)

**单元格设置**

| 名称         | 说明                                    |
| ---------- | ------------------------------------- |
| 编辑标签点击命令   |                                       |
| 默认值        | 配置选项卡的默认项。 使用“/”分隔项目，例如 “用户/配置/角色/任务” |
| 配置标签项      |                                       |
| 从数据库生成选项   |                                       |
| 风格类型       | 下边框线/选项卡/卡片                           |
| 位置         | 上/下/左/右，设置标签页头的位置                     |
| 平均分配宽度     | 必须位置为上/下，勾选后当前标签头会占满空间                |
| 可用操作       | 直接拖拽可用操作到页面上，自动生成操作单元格命令              |
| 可用操作-隐藏标签项 |                                       |

***

#### EL-标签

**使用场景**

*   使用标签进行选择和标记

**使用方式**

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_3c59aAPXi1.png)

**设计时**

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_d6zEeMyB5K.png)

**运行时**

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_mHDAXEo8f1.png)

**单元格设置**

| 名称       | 说明                                                                   |
| -------- | -------------------------------------------------------------------- |
| 编辑值变更命令  |                                                                      |
| 编辑标签点击命令 |                                                                      |
| 单元格权限    |                                                                      |
| 配置颜色列表   | 设置颜色列表，从上到下依次生效，用完时会从第一个颜色项重新开始，不断循环。&#xA;如果希望所有标签使用相同颜色，则颜色列表保留一项即可 |
| 默认值      |                                                                      |
| 分隔符      |                                                                      |
| 尺寸       | 大/中/小                                                                |
| 主题       | 深色/浅色/线框                                                             |
| 标签间距     | 默认10                                                                 |
| 允许添加标签   |                                                                      |
| 添加标签设置   | 设置添加标签按钮的宽度、按钮文本、间距等                                                 |
| 自动移除重复项  | 标签文本重复时，仅显示第一项                                                       |
| 显示边框描边   |                                                                      |
| 只读       |                                                                      |

***

#### EL-时间线

**使用场景**

*   创建时间线（时间轴图）

**使用方式**

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_2-4JTjG1Hd.png)

**设计时**

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_l-eMSUd6oL.png)

**运行时**

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_pFZuoTdR56.png)

**单元格设置**

| 名称            | 说明                                                                                                                                                                                                                                                                                                                                |
| ------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| 从数据库生成项目      | 是否从数据库生成项目                                                                                                                                                                                                                                                                                                                        |
| 配置项目/绑定数据源    |                                                                                                                                                                                                                                                                                                                                   |
| 排序            | 默认/按时间戳升序排序/按时间戳降序排序                                                                                                                                                                                                                                                                                                              |
| 节点尺寸          | 小/大                                                                                                                                                                                                                                                                                                                               |
| 隐藏时间戳         | 勾选后将隐藏时间线上的时间戳                                                                                                                                                                                                                                                                                                                    |
| 时间戳显示位置       | 必须未勾选隐藏时间戳。文本下方/文本上方                                                                                                                                                                                                                                                                                                              |
| 时间戳格式         | 必须未勾选隐藏时间戳。                                                                                                                                                                                                                                                                                                                       |
| 可用操作          | 直接拖拽可用操作到页面上，自动生成操作单元格命令                                                                                                                                                                                                                                                                                                          |
| 可用操作-设置数据源    | 使用动态 JSON 对象作为单元格的数据源，JSON 格式示例如下：&#xA;\[&#xA;{"content": "Activity start", "timestamp": "2018-04-15"},&#xA;{"content": "Approved", "timestamp": "2018-04-13"},&#xA;{"content": "Create Success", "timestamp": "2018-04-11"}&#xA;]&#xA;上面的数据假设“内容属性名”为content，“时间属性名”是timestamp&#xA;通常，JSON数据源可以通过 HTTP 请求命令从Web服务获取，也可以通过服务端命令获取 |
| 可用操作-重新加载绑定项目 |                                                                                                                                                                                                                                                                                                                                   |

***

#### EL-时间选择器

**优劣对比**

与活字格内置单元格类型-时间对比

优势：

*   更现代的UI界面

*   支持设置时间选择模式

*   支持时间范围选择

*   支持头部图标

*   支持清空按钮

劣势

*   不支持添加进表格

*   不支持获得焦点时全选文本内容

*   不支持设置单元格样式

*   不支持使用单元格格式

*   不支持下拉按钮显示设置

**使用方式**

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_UEORlgCCP0.png)

**设计时**

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_TcXazOFLEv.png)

**运行时**

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_9c6XndXQfX.png)

**单元格设置**

| 名称          | 说明                       |
| ----------- | ------------------------ |
| 编辑命令        |                          |
| 数据验证        |                          |
| 单元格权限       |                          |
| 默认值         |                          |
| 开始时间        | 设置可选择的开始时间，范围外时间可见但不可选择  |
| 结束时间        | 设置可选择的结束时间，范围外时间可见但不可选择  |
| 模式          | 固定时间点/任意时间点              |
| 范围选择        | 必须模式为任意时间点。选择开始时间和结束时间   |
| 开始时间占位文本    | 必须启用范围选择                 |
| 范围分隔符       | 必须启用范围选择                 |
| 开始时间占位文本    | 必须启用范围选择                 |
| 占位文本        | 必须禁用范围选择                 |
| 头部图标        |                          |
| 显示清空按钮      |                          |
| 文本框可输入      |                          |
| 只读          |                          |
| 禁用          |                          |
| 可用操作        | 直接拖拽可用操作到页面上，自动生成操作单元格命令 |
| 可用操作-获取选择范围 | 必须启用范围选择                 |

***

#### EL-穿梭框

**使用场景**

*   另一种形式的多选

**使用方式**

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_YPC-gM6DKr.png)

说明：对vue有基础知识的开发者，可以通过这个类型提供的一些接口，定义自己的UI。

*   customRender(h, option):

    1.  这个方法可以自定义UI

    2.  用户使用Javascript命令修改此方法

    3.  此函数种有两个参数：

        i.h，表示vue框架中的render方法，它会返回virtual dom

        ii.option，该参数来自于EL-穿梭框，表示穿梭框数据源中的每一项

    4.  此方法返回一个virtual dom

*   refreshUI：此方法用于在用户更改 render后重新构建EL-穿梭框，因为 vue 不知道渲染已更改，需要您手动刷新

*   getValueFromElement：获取单元格的值

**案例**

*   任务：创建一个UI类似下图的穿梭框

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_bJ9X1SDbAR.png)

*   步骤：

    1.  编辑页面加载命令

        （1选择“JavaScript命令”

        （2编辑javascript：

        *   给穿梭框设置一个单元格名称

        *   获取穿梭框的单元格类型

        *   定义个性化render

        *   更换穿梭框的render

        *   刷新UI

    2.  编码元素样式

        （1为穿梭框设定一个CSS类名

        （2编码CSS文件并提交

    3.  如何获取要提交的自定义值

        （1通过getValueFromElement获取单元格类型值

        （2通过cellType.vue.data获取穿梭框数据源

        （3通过自定义数据源和单元格类型值来定义自己的值

*   项目附件：[见Example project中的 EL-穿梭框.fgcc](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/blob/master/README_DEMO/EL-%E7%A9%BF%E6%A2%AD%E6%A1%86.fgcc)

**设计时**

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_r3z3IM-8lK.png)

**运行时**

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_t7PesVBzAB.png)

**单元格设置**

| 名称            | 说明                                |
| ------------- | --------------------------------- |
| 编辑命令          |                                   |
| 默认值           |                                   |
| 从数据库生成选项      |                                   |
| 配置选项/绑定数据源    |                                   |
| 左侧列表标题        |                                   |
| 右侧列表标题        |                                   |
| 右侧列表元素的排序策略   | 保持与数据源相同的顺序/新加入的元素排在最后/新加入的元素排在最前 |
| 可搜索           |                                   |
| 搜索框占位文本       |                                   |
| 可用操作          | 直接拖拽可用操作到页面上，自动生成操作单元格命令          |
| 可用操作-重新加载绑定项目 | 必须从数据库生成选项。                       |

***

#### EL-文件上传

**优劣对比**

与活字格内置单元格类型-附件对比

优势：

*   更现代的UI界面

劣势

*   不支持添加进表格

*   不支持使用单元格格式

*   不支持拖放上传文件

**使用方式**

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_bkoiGVzhbK.png)

**设计时**

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image__Y0k8n8cZR.png)

**运行时**

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_Rg1OjaQ_Dd.png)

**单元格设置**

| 名称            | 说明                                                                       |
| ------------- | ------------------------------------------------------------------------ |
| 单元格权限         |                                                                          |
| 最大允许上传个数      | 限制可以上传的最大文件数。 默认不限制。&#xA;如果用户尝试上传的文件数超过限制，将弹出警告                          |
| 上传的文件类型       | 接受上传文件的扩展名。 默认为“.jpg, .jpeg, .png”&#xA;如果用户尝试上传无效文件，将弹出警告                |
| 最大文件大小(单位：MB) | 上传文件大小限制，单位MB，默认1MB&#xA;删除值置空或将值设置为 0 均表示没有限制&#xA;如果用户尝试上传的文件数超过限制，将弹出警告 |
| 文件布局类型        | 文件名/图文列表/照片墙                                                             |
| 上传按钮文本        | 必须文件布局类型为文件名/图文列表                                                        |
| 提示信息          | 必须文件布局类型为文件名/图文列表                                                        |
| 支持在对话框中多选文件   |                                                                          |
| 禁用            |                                                                          |
| 只读            |                                                                          |

***

#### EL-表格

**使用场景**

*   使用element的表格类型

**使用方式**

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_kO_yxKEbFq.png)

**设计时**

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_A4CIA1I7as.png)

**运行时**

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image__jr4DULINH.png)

**单元格设置**

| 名称         | 说明                                               |
| ---------- | ------------------------------------------------ |
| 表格名        | EL-表格的唯一识别符，可以用于EL-分页中绑定表                        |
| 行点击命令      | 当行被单击时触发&#xA;自有参数：行数据。即被点击的行数据                   |
| 行双击命令      | 当行被双击时触发&#xA;自有参数：行数据。即被点击的行数据                   |
| 编辑当前行变更命令  | 当当前行变动时触发，行单击命令和行双击命令都可能触发&#xA;自有参数：行数据。即被点击的行数据 |
| 数据源        | 设定绑定的数据表                                         |
| 自动根据数据源生成列 |                                                  |
| 手动配置表格列    | 必须取消勾选自动根据数据源生成列。手动设置每列配置。                       |
| 显示操作按钮     | 是否于表格末尾显示操作按钮                                    |
| 配置操作按钮     | 配置操作按钮的行为和样式。                                    |
| 操作列宽度      |                                                  |
| 在表尾显示合计行   |                                                  |
| 合计行第一列的文本  |                                                  |
| 表格的尺寸      | 大\|中\|小                                          |
| 显示表头       |                                                  |
| 显示行号       |                                                  |
| 显示选择列      |                                                  |
| 列的宽度是否自撑开  |                                                  |
| 带有纵向边框     |                                                  |
| 斑马纹        |                                                  |
| 高亮当前行      |                                                  |
| 可用操作       | 直接拖拽可用操作到页面上，自动生成操作单元格命令                         |
| 可用操作-设置当前行 |                                                  |
| 可用操作-重载表格  |                                                  |


**补充说明**

对于基础的筛选和查询功能，在EL-表格中和原生的表格不太一样，具体解释如下：

*   表头筛选：取消勾选自动根据数据源生成列，在配置表格列中，勾选开启数据过滤，需要多选时勾选数据过滤是否多选。
*   表格查询：对于数据源绑定表的，直接将期望的查询语句设置在 设置数据源 界面中的查询条件中，并将空值查询策略设置为全部记录，这样就可以直接通过文本框进行查询。
![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_eltablequery.png)

***

***

## Element UI 命令

#### EL-通知

**使用场景**

*   在页面角落弹出提醒信息

**使用方式**

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_aaaTHc4Jru.png)

**效果示例**

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_Bb8LtrfhiX.png)

**命令选项**

| 名称          | 说明                          |
| ----------- | --------------------------- |
| 标题          |                             |
| 内容          |                             |
| 主题样式        | 成功\|警告\|信息\|失败              |
| 显示时长(单位:毫秒) | 显示时间，单位为毫秒。设置为 0 不会自动关闭。    |
| 弹出位置        | 右上角\|左下角\|右下角\|左下角          |
| 偏移距离(单位:像素) | 所有通知框实例都应该具有相同的偏移量。         |
| 显示关闭按钮      |                             |
| 上下文         | 用于存储将在点击通知命令中使用的上下文值（例如 ID） |
| 点击通知执行命令... |                             |

**主题样式**

*   成功

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_oOb2dWBVHb.png)

*   警告

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_aSMBLqq6NN.png)

*   信息

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_iomybhrYuw.png)

*   失败

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_p0hrcAxgyf.png)

***

#### EL-消息

**使用场景**

*   可以在用户活动后展示反馈信息，【EL-通知】常用于显示系统发送的被动通知。

**使用方式**

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_2WZnu9iMPZ.png)

**效果示例**

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_5_Oj7YraKs.png)

**命令选项**

| 名称          | 说明                       |
| ----------- | ------------------------ |
| 消息内容        |                          |
| 主题样式        | 成功\|警告\|信息\|失败           |
| 显示时长(单位:毫秒) | 显示时间，单位为毫秒。设置为 0 不会自动关闭。 |
| 偏移距离(单位:像素) | 设置消息框距离窗口顶部的偏移量          |
| 显示关闭按钮      |                          |
| 文字居中        |                          |

**主题样式**

*   成功

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_Rkm2C5ulN8.png)

*   警告

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_uTC9FnE2p3.png)

*   信息

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_Ite5ZZ8I-G.png)

*   失败

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_iPQ44uJ8jY.png)

***

#### EL-弹框

**使用场景**

*   更现代的消息框UI，可用于替换html警报或确认对话框

**使用方式**

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_YOyr6fCGA3.png)

**效果示例**

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_BxrQnJadxK.png)

**命令选项**

| 名称                 | 说明                                                                                                                                |
| ------------------ | --------------------------------------------------------------------------------------------------------------------------------- |
| 标题                 |                                                                                                                                   |
| 正文内容               |                                                                                                                                   |
| 主题样式               | 无\|成功\|警告\|信息\|失败                                                                                                                 |
| 显示确定按钮             |                                                                                                                                   |
| 确定按钮的文本            |                                                                                                                                   |
| 显示取消按钮             |                                                                                                                                   |
| 显示关闭按钮             |                                                                                                                                   |
| 弹框结果至变量            | 定义一个变量。 当消息框关闭时，将结果值保存到变量中。&#xA;然后，其他命令可以基于消息框结果进行不同的执行。&#xA;&#xA;单击确定返回结果为【confirm】&#xA;单击取消返回结果为【cancel】&#xA;单击关闭按钮返回结果为【close】 |
| 高级设置-显示输入框         |                                                                                                                                   |
| 高级设置-居中布局          |                                                                                                                                   |
| 高级设置-可通过点击遮罩关闭弹窗   |                                                                                                                                   |
| 高级设置-可通过按下ESC键关闭弹窗 |                                                                                                                                   |
| 高级设置-将取消与关闭进行区分    | 未勾选时，取消和关闭的返回结果都是【cancel】。勾选时，见*弹框结果至变量*(本表格第8行)                                                                                  |

**主题样式**

*   无

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_xn4-tp4PZy.png)

*   成功

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_new4UYkVhx.png)

*   警告

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_m8zuza4_ry.png)

*   信息

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_NE3ORrubXW.png)

*   失败

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_3PF7VUciMO.png)

***

***

## Element UI自定义

允许开发人员使用代码自定义组件UI

**策略**

*   支持使用html

*   支持使用vue模板

*   支持使用Element组件

**限制**

*   不支持使用虚拟节点

*   不支持使用自定义组件

***

#### 自定义EL-回到顶部

**说明**

*   允许开发者自定义回到顶部的UI

*   支持自定义回到顶部的内容

**使用指南**

*   在页面中设置一个【EL-回到顶部】类型的单元格

*   为该单元格赋一个CSS类名

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_qNxueOp4oD.png)

*   将你的javascript上传进设计器的“设置→自定义JavaScript/CSS”中

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_SWQL6tT78D.png)

*   代码演示

*   "back-top-custom-render" 表示回到顶部的 css 名称

*   请设置变量“FgcElement”、“BackTopSlots”、“Content”的名称与演示代码相同，否则将不起作用

```纯文本
var FgcElement = FgcElement || {};

FgcElement.BackTopSlots = FgcElement.BackTopSlots || {};

FgcElement.BackTopSlots.Content = FgcElement.BackTopSlots.Content || {};


FgcElement.BackTopSlots.Content["back-top-custom-render"] = function(){
    return "<div style='color: red;'>UP</div>";
}
```

*   设置前

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_pHQcqit6YX.png)

*   设置后

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_Re_oiUDLvE.png)

*   项目附件：[见Example project中的 EL-回到顶部.fgcc](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/blob/master/README_DEMO/EL-%E5%9B%9E%E5%88%B0%E9%A1%B6%E9%83%A8.fgcc)

***

#### 自定义EL-日历

**说明**

*   允许用户自定义日历标头与单元内容

**使用指南**

*   在页面中设置一个【EL-日历】类型的单元格

*   为该单元格赋一个CSS类名

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_EL4FsjrmDI.png)

*   将你的javascript上传进设计器的“设置→自定义JavaScript/CSS”中

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_BtBU4Ryhqh.png)

*   代码演示

*   “calendar-custom-render”是您为【EL-日历】指定的 CSS 名称。

*   请设置变量“FgcElement”、“CalendarSlots”、“Header”、“Cell”的名称与演示代码相同，否则将不起作用

*   定义日历标头的示例代码中的关键字“data”是 【EL-日历】中的内置数据

*   定义单元格内容的示例代码中的关键字“data”是 【EL-日历】中的内置数据

```纯文本
// data of header
interface data {
  date: String // current month
}
// data of cell
interface data {
  type: String,
   isSelected: Boolean,
   day: Number,
    date: String
}
```

```纯文本
var FgcElement = FgcElement || {};
 
FgcElement.CalendarSlots = FgcElement.CalendarSlots || {};
 
FgcElement.CalendarSlots.Header = FgcElement.CalendarSlots.Header || {};
 
FgcElement.CalendarSlots.Cell = FgcElement.CalendarSlots.Cell || {};
 
 
FgcElement.CalendarSlots.Header["calendar-custom-render"] = function(){
    return `
    <span>Custom header content</span>
      <span>{{ data.date }}</span>
      <el-button-group>
        <el-button size="small">Previous Year</el-button>
        <el-button size="small">Previous Month</el-button>
        <el-button size="small">Today</el-button>
        <el-button size="small">Next Month</el-button>
        <el-button size="small">Next Year</el-button>
      </el-button-group>
    `;
}
 
FgcElement.CalendarSlots.Cell["calendar-custom-render"] = function(){
    return `
    <p>
        {{ data.day.split('-').slice(1).join('-') }}
        {{ data.isSelected ? '✔️' : '' }}
      </p>
    `
}
```

*   设置前

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_diIkbG_g5_.png)

*   设置后

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_T8pmhv6bU5.png)

*   项目附件：[见Example project中的 EL-日历.fgcc](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/blob/master/README_DEMO/EL-%E6%97%A5%E5%8E%86.fgcc)

***

#### 自定义EL-级联选择器

**说明**

*   允许用户自定义级联节点UI

**使用指南**

*   在页面中设置一个【EL-级联选择器】类型的单元格

*   为该单元格赋一个CSS类名

*   将你的javascript上传进设计器的“设置→自定义JavaScript/CSS”中

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_dN0dTFZJ14.png)

*   代码演示

*   “cascader-custom-render”是您为【EL-级联选择器】指定的 CSS 名称。

*   请设置变量“FgcElement”、“CascaderSlots”、“Node”的名称与演示代码相同，否则将不起作用

*   示例代码中的关键字“data”和“node”是【EL-级联选择器】中的内置数据

```纯文本
interface data {
  node: Node,
  data: Data
}
// node and data are current Node object and node data respectively.

interface Data {
  value: String,
  label: String,
  children: Array<Data>
}
```

```纯文本
var FgcElement = FgcElement || {};
 
FgcElement.CascaderSlots = FgcElement.CascaderSlots || {};
 
FgcElement.CascaderSlots.Node = FgcElement.CascaderSlots.Node || {};
 
FgcElement.CascaderSlots.Node["cascader-custom-render"] = function(){
    return `
      <span>{{ data.label }}</span>
      <span v-if="!node.isLeaf"> ({{ data.children.length }}) </span>
      `
}
```

*   设置前

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_V4Qchekpaw.png)

*   设置后

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_Oaj6Csn5W3.png)

*   项目附件：[见Example project中的 EL-级联选择器.fgcc](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/blob/master/README_DEMO/EL-%E7%BA%A7%E8%81%94%E9%80%89%E6%8B%A9%E5%99%A8.fgcc)

***

#### 自定义EL-日期选择器

**说明**

*   允许用户自定义范围分隔符和单元格内容UI

**使用指南**

*   在页面中设置一个【EL-日期选择器】类型的单元格

*   为该单元格赋一个CSS类名

*   将你的javascript上传进设计器的“设置→自定义JavaScript/CSS”中

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_PfgXB_khyi.png)

*   代码演示

*   “**date-picker-custom-render**”是您为【EL-日期选择器】指定的 CSS 名称。

*   请设置变量“FgcElement”、“DatePickerSlots”、“RangeSeparater”、“Cell”的名称与演示代码相同，否则将不起作用

*   示例代码中的关键字“cell”是【EL-日期选择器】中每个日期单元格内的内置数据

```纯文本
interface cell {
  column: number
  customClass: string
  disabled: boolean
  end: boolean
  inRange: boolean
  row: number
  selected: Dayjs
  isCurrent: boolean
  isSelected: boolean
  start: boolean
  text: number
  timestamp: number
  date: Date
  dayjs: Dayjs
  type: 'normal' | 'today' | 'week' | 'next-month' | 'prev-month'
}
```

```纯文本
var FgcElement = FgcElement || {};

FgcElement.DatePickerSlots = FgcElement.DatePickerSlots || {};

FgcElement.DatePickerSlots.RangeSeparater = FgcElement.DatePickerSlots.RangeSeparater || {};

FgcElement.DatePickerSlots.Cell = FgcElement.DatePickerSlots.Cell || {};


FgcElement.DatePickerSlots.RangeSeparater["date-picker-custom-render"] = function(){
    return "<span>XXX</span>";
}

FgcElement.DatePickerSlots.Cell["date-picker-custom-render"] = function(){
    return "<div style='color: red;'>{{ cell.text }}</div>"
} 
```

*   设置前

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_304ais40Ou.png)

*   设置后

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_Rvp6igZimp.png)

*   项目附件：[见Example project中的 EL-日期选择器-.fgcc](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/blob/master/README_DEMO/EL-%E6%97%A5%E6%9C%9F%E9%80%89%E6%8B%A9%E5%99%A8.fgcc)

***

#### 自定义EL-导航菜单

**说明**

*   允许用户自定义数据源树下叶子节点的UI

**使用指南**

*   在页面中设置一个【EL-导航菜单】类型的单元格

*   为该单元格赋一个CSS类名

*   将你的javascript上传进设计器的“设置→自定义JavaScript/CSS”中

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_pK9V3tEJNr.png)

*   代码演示

*   “nav-menu-custom-render”是您为【EL-导航菜单】指定的 CSS 名称。

*   请设置变量“FgcElement”、“MenuItemSlots”、“Content”的名称与演示代码相同，否则将不起作用

*   示例代码中的关键字“item”是用户定义的每个叶子节点

```纯文本
var FgcElement = FgcElement || {};
 
FgcElement.MenuItemSlots = FgcElement.MenuItemSlots || {};
 
FgcElement.MenuItemSlots.Content = FgcElement.MenuItemSlots.Content || {};
 
FgcElement.MenuItemSlots.Content["nav-menu-custom-render"] = function(){
    return "<div style='color: red;'>{{ item.label}}</div>"
}
```

*   设置前

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_4JmIwNdtFo.png)

*   设置后

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_nJt9X5rP5I.png)

*   项目附件：[见Example project中的 EL-导航菜单.fgcc](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/blob/master/README_DEMO/EL-%E5%AF%BC%E8%88%AA%E8%8F%9C%E5%8D%95.fgcc)

***

#### 自定义EL-选择器

**说明**

*   允许用户使用【EL-选择器】的代码以自定义UI

*   支持自定义选择前缀和选项内容

**使用指南**

*   在页面中设置一个【EL-选择器】类型的单元格

*   为该单元格赋一个CSS类名

*   将你的javascript上传进设计器的“设置→自定义JavaScript/CSS”中

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_oPNEHz7Lyo.png)

*   代码演示

*   “select-custom-render”是您为【EL-选择器】指定的 CSS 名称。

*   请设置变量“FgcElement”、“SelectSlots”、“Prefix”、“Option”的名称与演示代码相同，否则将不起作用

*   示例代码中的关键字“Option”是数据源中的每一项

```纯文本
interface option
{

  value: number | string

  label: string

}
```

```纯文本
var FgcElement = FgcElement || {};

FgcElement.SelectSlots = FgcElement.SelectSlots || {};

FgcElement.SelectSlots.Prefix = FgcElement.SelectSlots.Prefix || {};

FgcElement.SelectSlots.Option = FgcElement.SelectSlots.Option || {};

FgcElement.SelectSlots.Prefix["select-custom-render"] = function(){
    return "<span>X</span>";
}

FgcElement.SelectSlots.Option["select-custom-render"] = function(){
    return "<div style='color: red;'>{{ option.label }}</div>";
}
```

*   设置前

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_RNs9uLhjCs.png)

*   设置后

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_CPRCEisfEH.png)

*   项目附件：[见Example project中的 EL-选择器.fgcc](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/blob/master/README_DEMO/EL-%E9%80%89%E6%8B%A9%E5%99%A8.fgcc)

***

#### 自定义EL-表格

**说明**

*   允许开发者自定义表格单元格的UI

**使用指南**

*   在页面中设置一个【EL-表格】类型的单元格

*   为该单元格赋一个CSS类名

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_TmheawOaGx.png)

*   将你的javascript上传进设计器的“设置→自定义JavaScript/CSS”中

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_W3vzW9BuO3.png)

*   代码演示

*   "column-custom-render" 表示回到顶部的 css 名称

*   设置 javascript 代码自定义表格单元格的外观，将表格单元格文本颜色变为红色。

```纯文本
var FgcElement = FgcElement || {};

FgcElement.TableColumnSlots = FgcElement.TableColumnSlots || {};

FgcElement.TableColumnSlots.Content = FgcElement.TableColumnSlots.Content || {};

FgcElement.TableColumnSlots.Content["column-custom-render"] = function (scope) {
    return `<div style="color: red;">{{ scope.row[scope.column.property] }}<div>`
}
```

*   设置前

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_CYLgsF6iJj.png)

*   设置后

![](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/raw/master/README_image/image_Zde2E6DJvD.png)

*   项目附件：见[Example project中的 EL-表格.fgcc](https://gitee.com/GrapeCity/hzg-element-plus-plugin-source-code/blob/master/README_DEMO/EL-%E8%A1%A8%E6%A0%BC.fgcc)

*   如果您对代码中的参数有任何疑惑，请参阅element-plus官方文档
