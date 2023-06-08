# Graphs Unity Package - Пакет Unity для работы с графами

# Содержание
* [Установка](#Установка)
	* [В виде unity модуля](#В-виде-unity-модуля)
	* [В виде исходников](#В-виде-исходников)
* [Работа с Графом](#Работа-с-Графом)
	* [Создание Графа](#Создание-Графа)
	* [Добавление вершин](#Добавление-вершин-графа)
	* [Добавление ребер](#Добавление-ребер)
* [Поиск пути](#Поиск-пути)
	* [Без учета веса ребра](#Без-учета-веса-ребра-поиск-в-глубинуширину)
	* [Алгоритм Дейкстры](#По-алгоритму-Дейкстры)
	* [Алгоритм Беллмана — Форда](#По-алгоритму-Беллмана-—-Форда)

# Установка

## В виде unity модуля
Поддерживается установка в виде unity-модуля через git-ссылку в PackageManager или прямое редактирование `Packages/manifest.json`:
```
"ru.freeteam.graphs": "https://github.com/bonospb/Graphs.git",
```

## В виде исходников
Код так же может быть склонирован или получен в виде архива со страницы релизов.

# Работа с Графом

## Создание Графа
```c#
// Создаем экземпляр Графа и указываем в качестве значения тип "string"
Graph<string> graph = new Graph<string>();
```

## Добавление вершин графа
```c#
// Добавляем вершину
graph.AddVertex("Vertex 0");

// Добавляем массив вершин
string[] vertices = { "Vertex 1", "Vertex 2", "Vertex 3", "Vertex 4" };
graph.AddVertices(vertices);
```

## Добавление ребер
```c#
// Ребро соединяюшее вершины "Vertex 0" и "Vertex 1" с весом 3,
graph.AddEdge("Vertex 0", "Vertex 1", 3);
// Ребро соединяюшее вершины "Vertex 0" и "Vertex 2" с весом ребра 12
graph.AddEdge("Vertex 0", "Vertex 2", 12);
// Ребро соединяюшее вершины "Vertex 2" и "Vertex 4" с весом ребра 5
graph.AddEdge("Vertex 2", "Vertex 4", 5);

// Добавление нескольких ребер для вершины "Vertex 1". 
// В случае если вес не указывается он берется равный 0
string[] neighbors = { "Vertex 3", "Vertex 4" };
graph.AddEdges("Vertex 1", neighbors);
```

# Поиск пути

## Без учета веса ребра (поиск в глубину/ширину)
Для изменения типа поиска пользуемся перечислением `GraphSearchTypes`.
Так же можно указать множество вершин для исключения из списка для обхода.
```c#
// 
var path = new SearchAlgorithm<string>(skillModel.Graph).Search("Vertex 0", "Vertex 4", GraphSearchTypes.DFS);
Debug.Log($"Depth-first search \n\t* Path: {path}");

string[] excludeVertices = { "v1", "v2" };
path = new SearchAlgorithm<string>(skillModel.Graph).Search("Vertex 0", "Vertex 4", GraphSearchTypes.BFS, excludeVertices);
Debug.Log($"Breadth-first search \n\t* Path: {path}");
```

## По алгоритму Дейкстры
> **ВАЖНО!** Использование отрицательных весов ребер приведет к рекурсии в алгоритме со всеми вытекающими.
```c#
var path = new DijkstraAlgorithm<string>(skillModel.Graph).FindShortestPath("skill_base", "skill_07");
Debug.Log($"Dijkstra \n\t* Path: {path}");
```
 
## По алгоритму Беллмана — Форда
```c#
var path = new BellmanFordAlgorithm<string>(skillModel.Graph).FindShortestPath("skill_base", "skill_07");
Debug.Log($"BellmanFord \n\t* Path: {path}");
```

