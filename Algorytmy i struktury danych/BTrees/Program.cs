using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Jan Bienias
//238201
//
//Task ALZ.7.4
//https://inf.ug.edu.pl/~zylinski/dydaktyka/AiSD/ALZ_07.pdf
//
//Scientific sources :
//http://www.geeksforgeeks.org/b-tree-set-1-insert-2/
//http://www.geeksforgeeks.org/b-tree-set-3delete/

namespace BTrees
{
    class Program
    {
        static void Main(string[] args)
        {
            var t = new BTree(4);
            t.insert(10);
            t.insert(20);
            t.insert(5);
            t.insert(6);
            t.insert(12);
            t.insert(30);
            t.insert(7);
            t.insert(17);
            t.print();
            int k = 15;
            Console.WriteLine();
            if(t.search(k) != null)
                Console.WriteLine(k + " is present");
            else
                Console.WriteLine(k + " is not present");
            t.remove(17);
            t.print();
        }
    }

    public class BTreeNode
    {
        public int[] _k; // keys
        public BTreeNode[] _c; // childrens
        public bool _leaf { get; set; } //if node is a leaf (true / false)
        public int _t; //minimum degree
        public int _n; //current number of occupied slots in _k (keys)

        public BTreeNode(int t, bool leaf)
        {
            _t = t;
            _leaf = leaf;
            _k = new int [2 * t - 1];
            _c = new BTreeNode[2 * t];
            _n = 0;
        }

        public int findKey(int k)
        {
            //returns index of the first key that is greater/equal than k
            int i = 0;
            while (i < _n && _k[i] < k)
                ++i;
            return i;
        }

        public int getPred(int i)
        {
			//moving to the right most node until we reach a leaf
            BTreeNode current = _c[i];
            while (!current._leaf)
                current = current._c[current._n];
            return current._k[current._n - 1];
        }

        public int getSucc(int i)
        {
			//moving to the left most node starting from _c[i+1] until we reach a leaf
            BTreeNode current = _c[i + 1];
            while (!current._leaf)
                current = current._c[0];
            return current._k[0];
        }

        public void borrowFromPrev(int i)
        {
			// The last key from _c[i-1] goes up to the parent and key[i-1]
			// from parent is inserted as the first key in _c[i]. Thus, the loses
			// sibling one key and child gains one key
            BTreeNode child = _c[i];
            BTreeNode sibling = _c[i - 1];
			// moving all key in _c[i] one step ahead
            for (int j = child._n - 1; j >= 0; --j)
                child._k[j + 1] = child._k[j];
			// ff _c[i] is not a leaf, move all its child pointers one step ahead
            if(!child._leaf)
            {
                for (int j = child._n; j >= 0; --j)
                    child._c[j + 1] = child._c[j];
            }
			//setting child's first key equal to keys[i-1] from the current node
            child._k[0] = _k[i - 1];
			// move sibling's last child as C[idx]'s first child
            if (!_leaf)
                child._c[0] = sibling._c[sibling._n];
			// Moving the key from the sibling to the parent
			// This reduces the number of keys in the sibling
            _k[i - 1] = sibling._k[sibling._n - 1];
            child._n += 1;
            sibling._n -= 1;
            return;
        }

        public void borrowFromNext(int i)
        {
			// A function to borrow a key from the _c[i+1] and place
			// it in _c[i]
            BTreeNode child = _c[i];
            BTreeNode sibling = _c[i + 1];
            child._k[child._n] = _k[i];
			// sibling's first child is inserted as the last child into _c[i]
            if (!child._leaf)
                child._c[child._n + 1] = sibling._c[0];
			//The first key from sibling is inserted into keys[idx]
            _k[i] = sibling._k[0];
			// move all keys in sibling one step behind
            for (int j = 1; j < sibling._n; ++j)
                sibling._k[j - 1] = sibling._k[j];
			// move the child pointers one step behind
            if(!sibling._leaf)
            {
                for (int j = 1; j <= sibling._n; ++j)
                    sibling._c[j - 1] = sibling._c[j];
            }

            child._n += 1;
            sibling._n -= 1;
            return;
        }

        public void merge(int i)
        {
            BTreeNode child = _c[i];
            BTreeNode sibling = _c[i + 1];
            //pull a key from the current node and insert it into (t-1) position of child[i]
            child._k[_t - 1] = _k[i];
            //copying keys from child[i+1] to c[i] at the end
            for (int j = 0; j < sibling._n; ++j)
                child._k[j + _t] = sibling._k[j];
            //copying child refs from child[i+1] to c[i]
            if(!child._leaf)
            {
                for (int j = 0; j <= sibling._n; ++j)
                    child._c[j + _t] = sibling._c[j];
            }
            //move all keys after (i) in current node by one step before to fill the gap
            for (int j = i + 1; j < _n; ++j)
                _k[j - 1] = _k[j];
            //move the child refs after (i+1) in current node one step before
            for (int j = i + 2; j <= _n; ++j)
                _c[j - 1] = _c[j];

            child._n += sibling._n + 1;
            _n--;
            return;
        }

        public void fill(int i)
        {
			//function to fill child _c[i] which has less than t-1 keys :
			
			// if the previous child has more than t-1 keys, borrow a key
			// from that child
            if (i != 0 && _c[i - 1]._n >= _t)
                borrowFromPrev(i);
			// if the next child(_c[i+1]) has more than t-1 keys, borrow a key
			// from that child
            else if (i != _n && _c[i + 1]._n >= _t)
                borrowFromNext(i);

            else
			//Merge
			// if _c[i] is the last child, merge it with with its previous sibling
			// otherwise merge it with its next sibling
            {
                if (i != _n)
                    merge(i);
                else
                    merge(i - 1);
            }
            return;
        }
            
        public void removeFromLeaf(int i)
        {
            //move all keys after the i-th position one place backward
            for (int j = i + 1; j < _n; ++j)
                _k[j - 1] = _k[j];
            _n--;
            return;
        }

        public void removeFromNonLeaf(int i)
        {
			// If the child that precedes k (_c[i]) has atleast t keys,
			// find the predecessor 'pred' of k in the subtree rooted at
			// _c[i]. Replace k by pred. Recursively delete pred
			// in _c[i]
            int k = _k[i];
            if(_c[i]._n >= _t)
            {
                int pred = getPred(i);
                _k[i] = pred;
                _c[i].remove(pred);
            }
			 // If the child _c[i] has less that t keys, examine _c[i+1].
			// If _c[i+1] has atleast t keys, find the successor 'succ' of k in
			// the subtree rooted at _c[i+1]
			// Replace k by succ
			// Recursively delete succ in _c[i+1]
            else if (_c[i+1]._n >= _t)
            {
                int succ = getSucc(i);
                _k[i] = succ;
                _c[i + 1].remove(succ);
            }
			 // If both _c[i] and _c[i+1] has less that t keys,merge k and all of _c[i+1]
            else
            {
                merge(i);
                _c[i].remove(k);
            }
            return;
        }

        public void remove(int k)
        {
            int i = findKey(k);
            if(i < _n && _k[i] == k)
            {
                if (_leaf)
                    removeFromLeaf(i);
                else
                    removeFromNonLeaf(i);
            }
            else
            {
                if(_leaf)
                {
                    Console.WriteLine("Key " + k + " does not exist");
                    return;
                }
                //Flag indicates wheter or not the key is present in the sub-tree
                //rooted with the last child of this node
                bool flag = ((i == _n) ? true : false);
                //if the child where the k key is supposed to exist has less than t keys
                //we have to fill that child
                if (_c[i]._n < _t)
                    fill(i);
                //if the last child has been merged it must have been merged with previous child
                //we recurse on the (i-1) child or we recurse on the (i) child which now has at least
                //t keys
                if (flag && i > _n)
                    _c[i - 1].remove(k);
                else
                    _c[i].remove(k);
            }
            return;   
        }

        public void print()
        {
            int i;
            for(i = 0; i < _n; i++)
            {
                if (_leaf == false)
                    _c[i].print();
                Console.Write(_k[i] + " ");
            }
            if (_leaf == false)
                _c[i].print();
        }

        public BTreeNode search(int k)
        {
            int i = 0;
            //find the first key that is greater/equal than k
            while (i < _n && k > _k[i])
                i++;

            if (_k[i] == k)
                return this;

            if (_leaf == true)
                return null;

            return _c[i].search(k);
        }

        public void splitChild(int i, BTreeNode y)
        {
            BTreeNode z = new BTreeNode(y._t , y._leaf);
            z._n = _t - 1;
            //copy last (t-1) keys of y to z
            for (int j = 0; j < _t - 1; j++)
                z._k[j] = y._k[j + _t];
            //copy last t children of y to z
            if(y._leaf == false)
            {
                for (int j = 0; j < _t; j++)
                    z._c[j] = y._c[j + _t];
            }
            y._n = _t - 1;
            //create space for the new child - push everything + 1
            for (int j = _n; j >= i + 1; j--)
                _c[j + 1] = _c[j];
            //linking new child to this node
            _c[i + 1] = z;
            //create space for the key of y
            for (int j = _n - 1; j >= i; j--)
                _k[j + 1] = _k[j];
            //copy middle key of y to this node
            _k[i] = y._k[_t - 1];
            _n = _n + 1;
        }

        public void insertNonFull(int k)
        {
            int i = _n - 1;
            if(_leaf == true)
            {
                //find the location of new key to be inserted
                //move eveything one place ahead
                while(i >= 0 && _k[i] > k)
                {
                    _k[i + 1] = _k[i];
                    i--;
                }
                //insert
                _k[i + 1] = k;
                _n = _n + 1;
            }
            else
            {
                //find the child which is going to have the new key
                while (i >= 0 && _k[i] > k)
                    i--;
                //check if the found child is full
                if(_c[i+1]._n == 2*_t - 1)
                {
                    splitChild(i + 1, _c[i + 1]);
                    //after split the middle key of _c[i] goes up...
                    //check which of the two is going to get the new key
                    if (_k[i + 1] < k)
                        i++;
                }
                _c[i + 1].insertNonFull(k);
            }
        }
    }

    public class BTree
    {
        private BTreeNode _root;
        public static int _t; //minimum degree

        public BTree(int t)
        {
            _root = null;
            if(t%2 == 0)
            {
                Console.WriteLine("Degree is not even! degree++");
                _t = t + 1;
            }
            else
                _t = t;
        }

        public void print()
        {
            if (_root != null)
                _root.print();
        }

        public BTreeNode search(int k)
        {
            if (_root == null)
                return null;
            else
                return _root.search(k);
        }

        public void remove(int k)
        {
            if(_root == null)
            {
                Console.WriteLine("Tree is empty!");
                return;
            }
            _root.remove(k);
            if(_root._n == 0)
            {
                BTreeNode tmp = _root;
                if (_root._leaf)
                    _root = null;
                else
                    _root = _root._c[0];
            }
            return;
        }

        public void insert(int k)
        {
            if(_root == null)
            {
                _root = new BTreeNode(_t, true);
                _root._k[0] = k;
                _root._n = 1;
            }
            else
            {
                if (_root._n == 2 * _t - 1)
                {
                    BTreeNode s = new BTrees.BTreeNode(_t, false);
                    //make old root as a child of new root
                    s._c[0] = _root;
                    //split the old root and move 1 key to the new root
                    s.splitChild(0, _root);
                    //root at this moment has two children now
                    int i = 0;
                    if (s._k[0] < k)
                        i++;
                    s._c[i].insertNonFull(k);
                    _root = s;
                }
                else
                    _root.insertNonFull(k);
            }
     
        }

    }
}
