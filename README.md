# Todo.MVVM.Learning

### 📖 项目简介

这是一个用于学习 **WPF MVVM 开发模式** 的练习项目。
 项目包含两个实现版本：

1. **Native MVVM**：使用手写 `INotifyPropertyChanged`、`ICommand` 等基础实现，理解 MVVM 的核心原理。
2. **Toolkit MVVM**：使用 **CommunityToolkit.MVVM** 简化样板代码，通过属性生成器、命令生成器和依赖注入等特性，提高开发效率。

通过这个项目，可以直观对比 **原生 MVVM 与 框架 MVVM** 的差异，理解 MVVM 的本质，并逐步掌握在实际项目中使用框架的最佳实践。

### 🎯 学习目标

- 熟悉 MVVM 的两大支柱：
  - **属性通知**（INotifyPropertyChanged / RaisePropertyChanged）
  - **命令绑定**（ICommand / RelayCommand）
- 理解 WPF 的数据绑定、命令绑定机制
- 掌握 ObservableCollection 与 UI 更新的关系
- 学会使用 CommunityToolkit.MVVM 的 `[ObservableProperty]`、`[RelayCommand]` 等特性
- 能够独立完成一个小型 WPF 应用的开发

### 🚀 迭代路线（从简单到复杂）

1. **基础功能**✔️
   - 添加 / 删除待办事项
   - 保存 / 加载（JSON 文件存储）
2. **功能优化**✔️
   - 按完成状态筛选、排序
   - 添加截止日期、优先级
3. **数据持久化**
   - 使用 SQLite 存储待办数据
   - 尝试引入 ORM（例如 Dapper / EF Core）
4. **界面优化**
   - 数据模板美化
   - 动态样式（已完成任务加删除线 / 淡化显示）
   - 多窗口或页面导航
5. **进阶功能**
   - 添加任务分类
   - 导出任务数据（CSV / Excel）
   - 消息通知机制（任务到期提醒）
