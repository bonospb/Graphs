# Graphs Unity Package - Пакет Unity для работы с графами

# Содержание
* [Установка](#Установка)
	* [В виде unity модуля](#В-виде-unity-модуля)
	* [В виде исходников](#В-виде-исходников)
* [Работа с Графом](#Работа-с-Графом)
	* [Создание Графа](#Создание-Графа)
	* [Добавление вершин](#Добавление-вершин-графа)
* [Обычный поиск](#Обычный-поиск)
* [Алгоритм Дейкстры](#Поиск-по-алгоритму-Дейкстры)
* [Алгоритм Беллмана — Форда](#Поиск-по-Алгоритму-Беллмана-—-Форда)

# Установка

## В виде unity модуля
Поддерживается установка в виде unity-модуля через git-ссылку в PackageManager или прямое редактирование `Packages/manifest.json`:
```
"ru.freeteam.graphs": "https://github.com/bonospb/Graphs.git",
```

## В виде исходников
Код так же может быть склонирован или получен в виде архива со страницы релизов.

#Работа с Графом

## Создание Графа
```c#

```

## Добавление вершин графа
```c#

```

# Обычный поиск


# Поиск по алгоритму Дейкстры

> **ВАЖНО!** Использование отрицательных весов ребер приведет к рекурсии в алгоритме со всеми вытекающими.
 
# Поиск по Алгоритму Беллмана — Форда
