namespace ElementCellTypes {

    export interface IItem {
        value: any;
        label: any;
        parentValue: any;
    }

    export interface ITreeNodeBase {
        value: any;
        children: ITreeNodeBase[];
    }

    interface INode extends ITreeNodeBase {
        value: any;
        label: any;
        parentValue: any;
        uId: any;
        children: INode[];
    }

    export class TreeHelper {

        public static build(nodes: IItem[]) {
            if (!nodes) {
                return undefined;
            }
            const allDataNodes = nodes.map(item => <INode>{
                ...item
            });
            for (const node of allDataNodes) {
                node.uId = this.getUid(node.value, node.parentValue);
            }
            const uidNodesCache = <{ [uid: number]: INode }>{};
            const idNodesCache = <{ [id: string]: INode[] }>{};
            const parentIdNodesCache = <{ [pid: string]: INode[] }>{};
            for (const node of allDataNodes) {
                uidNodesCache[node.uId] = node;
            }
            for (const node of allDataNodes) {
                if (!idNodesCache[node.value]) {
                    idNodesCache[node.value] = [];
                }
                idNodesCache[node.value].push(node);
            }
            for (const node of allDataNodes) {
                if (!parentIdNodesCache[node.parentValue]) {
                    parentIdNodesCache[node.parentValue] = [];
                }
                parentIdNodesCache[node.parentValue].push(node);
            }

            this.buildChildren(allDataNodes, parentIdNodesCache);

            const roots = allDataNodes.filter(i => this.isRootNode(i, idNodesCache));

            const exsitId = {};

            const uniqueRoots = roots.filter(i => {
                if (exsitId[i.value]) {
                    return false;
                }
                exsitId[i.value] = true;
                return true;
            });
            return uniqueRoots;
        }

        private static getUid(id: string, pid: string) {
            return (id ?? "") + "_%_" + (pid ?? "");
        }

        private static buildChildren(nodes: INode[], parentIdDataNodesCache: { [pid: string]: INode[] }) {
            for (const node of nodes) {
                const children = parentIdDataNodesCache[node.value];
                if (children) {
                    node.children = children;
                } else {
                    node.children = null;
                }
            }
        }

        private static isRootNode(node: INode, idNodesCache: { [id: string]: INode[] }) {
            if (!node.parentValue) {
                return true;
            }
            const nodes = idNodesCache[node.parentValue];
            if (!nodes) {
                return true;
            }
            return false;
        }

        public static flat(nodes: ITreeNodeBase[]): ITreeNodeBase[] {
            const result = <ITreeNodeBase[]>[];
            if (nodes && nodes.length) {
                for (const n of nodes) {
                    result.push(n);
                    const subResult = this.flat(n.children);
                    subResult.every(i => result.push(i));
                }
            }
            return result;
        }
    }
}