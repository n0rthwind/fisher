using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace FISHER
{
    /// <summary>
    /// Класс для отображения дерева объектов, 
    /// с возможностью менять цвет корневого узла 
    /// в зависимости от состояния связи с объектами.
    /// </summary>
    public class MyTreeView : System.Windows.Forms.TreeView
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="connection"></param>
        public void NodeConnectionState(string name, bool connection)
        {
            // Ищем все элементы дерева с названиями как у модуля
            var nodes = this.Nodes[0].Nodes.OfType<TreeNode>()
                .Where(n => n.Text.Substring(0, name.Length).ToLower() == name.ToLower());

            // Определяем цвет для узла и вершины дерева
            Color color;
            if (connection)
            {
                color = Color.DarkGreen;
                DisconnectObjects.Remove(name);
                // Изменяем цвет корневого узла
                if (DisconnectObjects.Count == 0) 
                    this.Nodes[0].ForeColor = Color.DarkGreen;
                else
                    if ((DisconnectObjects.Count == 1) || (DisconnectObjects.Count == DataServer.modulNevod.Count - 1))
                        this.Nodes[0].ForeColor = Color.DarkRed;
            }
            else
            {
                color = Color.Red;
                if (DisconnectObjects.Find(_name => _name == name) == null) { DisconnectObjects.Add(name); }
                // Зменяем цвет корневого узла, если еще не делали этого
                if ((DisconnectObjects.Count == 1) || (DisconnectObjects.Count == DataServer.modulNevod.Count - 1))
                    this.Nodes[0].ForeColor = Color.DarkRed;
                else 
                    if (DisconnectObjects.Count == DataServer.modulNevod.Count)
                        this.Nodes[0].ForeColor = Color.Red;
            }

            // Изменяем цвет узлов отображающих состояние связи с данным модулем
            foreach (TreeNode node in nodes)
            {
                node.ForeColor = color;
            }
        }

        /// <summary>
        /// Список всех объектов, с которыми нет связи.
        /// </summary>
        public List<string> DisconnectObjects = new List<string>();
    }
}
