using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static FourJ.UserInterface;
using static FUI_Studio.Forms.Form1;
using FUI_Studio.Classes.fui;

namespace FUI_Studio.Classes
{
    static class FUIUtil
    {
        public static TreeNode ConstructFuiTreeNode(in FUIFile fui)
        {
            TreeNode rootNode = new TreeNode(fui.header.ImportName);
            rootNode.ImageIndex = (int)eTreeViewImgTag.FolderIcon;

            TreeNode[] nodes = { 
                new TreeNode("Header",  (int)eTreeViewImgTag.BaseIcon,    (int)eTreeViewImgTag.Selected),
                new TreeNode("Shapes",  (int)eTreeViewImgTag.FolderIcon,  (int)eTreeViewImgTag.Selected),
                new TreeNode("Sprites", (int)eTreeViewImgTag.FolderIcon,  (int)eTreeViewImgTag.Selected),
                new TreeNode("Images",  (int)eTreeViewImgTag.FolderIcon,  (int)eTreeViewImgTag.Selected),
                new TreeNode("Frames",  (int)eTreeViewImgTag.FolderIcon,  (int)eTreeViewImgTag.Selected),
                new TreeNode("Others",  (int)eTreeViewImgTag.FolderIcon,  (int)eTreeViewImgTag.Selected),
            };
            CreateNodesInternally(fui, nodes);
            rootNode.Nodes.AddRange(nodes);

            return rootNode;
        }


        private static TreeNode[] createShapeNodes(FUIFile fui)
        {
            var children = new List<TreeNode>();

            foreach (var shape in fui.shapes)
            {
                TreeNode node = new TreeNode();
                node.Tag = FuiShapeWrapper.CreateImageFromShape(shape, fui);
                children.Add(node);
            }

            return children.ToArray();
        }

        public static TreeNode[] CreateImageNodes(FUIFile fui)
        {
            var children = new List<TreeNode>();
            foreach (var symbol in fui.symbols)
            {
                if (symbol == null) throw new NullReferenceException();
                if (symbol.ObjectType != eFuiObjectType.BITMAP) continue;
                
                var imgNode = new TreeNode(symbol.Name);
                imgNode.Tag = fui.bitmaps[symbol.Index];
                imgNode.ImageIndex = (int)eTreeViewImgTag.ImgIcon;
                children.Add(imgNode);
            }
            return children.ToArray();
        }


        private static void CreateNodesInternally(FUIFile fui, TreeNode[] nodes)
        {
            nodes[0].Tag = fui.header;
            foreach (var symbol in fui.symbols)
            {
                if (symbol == null) throw new NullReferenceException();
                if (symbol.ObjectType != eFuiObjectType.TIMELINE) continue;

                var subNode = new TreeNode(symbol.Name);
                var timeline = fui.timelines[symbol.Index];
                subNode.ImageIndex = (int)eTreeViewImgTag.FolderIcon;

                if (timeline.frameCount == 1 && timeline.actionCount == 0)
                {
                    subNode.ImageIndex = (int)eTreeViewImgTag.ElementIcon;
                    var frame = fui.timelineFrames[timeline.frameIndex];
                    var fuiEvent = fui.timelineEvents[frame.EventIndex];
                    if (fuiEvent.NameIndex > -1)
                    {
                        string fuiEventName = fui.timelineEventNames[fuiEvent.NameIndex].EventName;
                        subNode.Text = fuiEventName;
                    }
                    switch (fuiEvent.ObjectType)
                    {
                        case eFuiObjectType.SHAPE:
                            subNode.Tag = FuiShapeWrapper.CreateImageFromShape(fui.shapes[fuiEvent.Index], fui);
                            nodes[1].Nodes.Add(subNode);
                            break;

                        case eFuiObjectType.TIMELINE:
                            nodes[2].Nodes.Add(subNode);
                            break;
                    }
                    continue;
                }
                subNode.Tag = timeline;
                nodes[4].Nodes.Add(subNode);
            }
            nodes[3].Nodes.AddRange(CreateImageNodes(fui));
        }
    }
}
