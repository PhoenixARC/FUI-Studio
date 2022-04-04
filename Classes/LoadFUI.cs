using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FUI_Studio.Classes.fui;
using static FUI_Studio.Forms.Form1;

namespace FUI_Studio.Classes
{
    // TODO find better class name
    static class FUIUtil
    {
        private static void ExtendTreeNode<T>(in TreeNode node, in List<T> dataList,string  SubNodePrefix)
        {
            if (dataList.Count == 0) return;
            if (node.ImageIndex != (int)eTreeViewImgTag.FolderIcon)
                node.ImageIndex = (int)eTreeViewImgTag.FolderIcon;
            for (int i = 0; i < dataList.Count; i++)
            {
                FUITreeNode subNode = new FUITreeNode($"{SubNodePrefix}{i}");
                subNode.ImageIndex = (int)eTreeViewImgTag.BaseIcon;
                node.Nodes.Add(subNode);
            }
        }

        private static FUITreeNode fastConstructNode<T>(in List<T> dataList, string NodeName, string SubNodePrefix)
        {
            if (dataList.Count == 0) return null;
            FUITreeNode fastConstructedNode = new FUITreeNode(NodeName);
            fastConstructedNode.ImageIndex = (int)eTreeViewImgTag.FolderIcon;
            ExtendTreeNode(fastConstructedNode, dataList, SubNodePrefix);
            return fastConstructedNode;
        }

        public static FUITreeNode ConstructFUITreeNode(in FourJ.UserInterface.FUIFile fui, int FuiIndex)
        {
            FUITreeNode rootNode = new FUITreeNode(fui.header.SwfFileName.Replace("\0", ""));

            rootNode.Tag = FuiIndex;
            rootNode.ImageIndex = (int)eTreeViewImgTag.FolderIcon;

            var timelineNode = fastConstructNode(fui.timelines, "Timelines", "fuiTimeline");
            if (timelineNode != null)
                rootNode.Nodes.Add(timelineNode);
            
            var timelineFrameNode = fastConstructNode(fui.timelineFrames, "TimelineFrames", "fuiTimelineFrame");
            if (timelineFrameNode  != null)
                rootNode.Nodes.Add(timelineFrameNode);

            var timelineEventNode = fastConstructNode(fui.timelineEvents, "TimelineEvents", "fuiTimelineEvent");
            if (timelineEventNode != null)
                rootNode.Nodes.Add(timelineEventNode);

            var timelineEventNameNode = fastConstructNode(fui.timelineEventNames, "TimelineEventNames", "fuiTimelineEventName");
            if (timelineEventNameNode != null)
                rootNode.Nodes.Add(timelineEventNameNode);

            var timelinActionNode = fastConstructNode(fui.timelineActions, "TimelineActions", "fuiTimelineAction");
            if (timelinActionNode != null)
                rootNode.Nodes.Add(timelinActionNode);

            var shapeNode = fastConstructNode(fui.shapes, "Shapes", "fuiShape");
            if (shapeNode != null)
                rootNode.Nodes.Add(shapeNode);

            var shapeComponentNode = fastConstructNode(fui.shapeComponents, "ShapeComponents", "fuiShapeComponent");
            if (shapeComponentNode != null)
                rootNode.Nodes.Add(shapeComponentNode);

            var vertNode = fastConstructNode(fui.verts, "Verts", "fuiVert");
            if (vertNode != null)
                rootNode.Nodes.Add(vertNode);

            var refNode = fastConstructNode(fui.references, "References", "fuiReference");
            if (refNode != null)
                rootNode.Nodes.Add(refNode);
            
            var edittextNode = fastConstructNode(fui.edittexts, "EditTexts", "fuiEditText");
            if (edittextNode != null)
                rootNode.Nodes.Add(edittextNode);
            
            var fontNameNode = fastConstructNode(fui.fontNames, "FontNames", "fuiFontName");
            if (fontNameNode != null)
                rootNode.Nodes.Add(fontNameNode);
            
            var symbolNode = fastConstructNode(fui.symbols, "Symbols", "fuiSymbol");
            if (symbolNode != null)
                rootNode.Nodes.Add(symbolNode);
            
            var importAssetNode = fastConstructNode(fui.importAssets, "ImportAssets", "fuiImportAsset");
            if (importAssetNode != null)
                rootNode.Nodes.Add(importAssetNode);

            var bitmapNode = fastConstructNode(fui.bitmaps, "Bitmaps", "fuiBitmap");
            if (bitmapNode != null)
                rootNode.Nodes.Add(bitmapNode);

            return rootNode;
        }
    }
}
