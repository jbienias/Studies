#Jan Bienias
#238201

require 'bst'

describe 'BinarySearchTree' do
  describe '#raises errors when BinarySearchTree not initialized properly' do
    it 'raises an ArgumentError, when added value is a string' do
      tree = BinarySearchTree.new
      expect { tree.insert('AVSAE') }.to raise_error(ArgumentError)
    end
    it 'raises an ArgumentError, when added value is a partially correct str' do
      tree = BinarySearchTree.new
      expect { tree.insert('AVS55522AE') }.to raise_error(ArgumentError)
    end
  end
  describe '#initializes properly' do
    it 'initializes correctly with nil value on root' do
      tree = BinarySearchTree.new
      expect(tree.root).to equal nil
    end
  end
  describe '#properly puts correct first value as a root value' do
    it 'inserts integer for a new tree as a first value' do
      tree = BinarySearchTree.new
      tree.insert(5)
      expect(tree.root.value).to equal 5.0
    end
    it 'inserts float for a new tree as a first value' do
      tree = BinarySearchTree.new
      tree.insert(10.44)
      expect(tree.root.value).to equal 10.44
    end
    it 'inserts string(numeric) for a new tree as a first value' do
      tree = BinarySearchTree.new
      tree.insert('111')
      expect(tree.root.value).to equal 111.0
    end
  end
  describe '#inserts correct values according to their size' do
    it 'inserts integers properly depending on values' do
      tree = BinarySearchTree.new
      tree.insert(10)
      tree.insert(12)
      tree.insert(9)
      expect(tree.root.value).to equal 10.0
      expect(tree.root.left.value).to equal 9.0
      expect(tree.root.right.value).to equal 12.0
    end
    it 'inserts floats properly depending on values' do
      tree = BinarySearchTree.new
      tree.insert(1.11)
      tree.insert(0.22)
      tree.insert(2.22)
      expect(tree.root.value).to equal 1.11
      expect(tree.root.left.value).to equal 0.22
      expect(tree.root.right.value).to equal 2.22
    end
    it 'inserts strings(numeric) properly depending on values' do
      tree = BinarySearchTree.new
      tree.insert('111.1')
      tree.insert('22.2')
      tree.insert('333.33')
      expect(tree.root.value).to equal 111.1
      expect(tree.root.left.value).to equal 22.2
      expect(tree.root.right.value).to equal 333.33
    end
    it 'inserts floats, ints and strings properly depending on values' do
      tree = BinarySearchTree.new
      tree.insert('31.11')
      tree.insert(2)
      tree.insert(55.62)
      expect(tree.root.value).to equal 31.11
      expect(tree.root.left.value).to equal 2.0
      expect(tree.root.right.value).to equal 55.62
    end
  end
  describe '#raises error, when some values are already present in tree' do
    it 'raises an ArgumentError, when value is already in tree' do
      tree = BinarySearchTree.new
      tree.insert('22.22')
      expect { tree.insert(22.22) }.to raise_error(ArgumentError)
    end
  end
  describe '#inserts values to right/left side only if values are increasing/decrasing' do
    it 'inserts everything to left side, when values are descending' do
      tree = BinarySearchTree.new
      tree.insert('10.22')
      tree.insert(9)
      tree.insert(5.2)
      expect(tree.root.value).to equal 10.22
      expect(tree.root.right).to equal nil
      expect(tree.root.left.value).to equal 9.0
      expect(tree.root.left.left.value).to equal 5.2
    end
    it 'inserts everything to the right side, when values are ascending' do
      tree = BinarySearchTree.new
      tree.insert(1)
      tree.insert('3.33')
      tree.insert(6.9)
      expect(tree.root.value).to equal 1.0
      expect(tree.root.left).to equal nil
      expect(tree.root.right.value).to equal 3.33
      expect(tree.root.right.right.value).to equal 6.9
    end
    it 'inserts everything properly' do
      tree = BinarySearchTree.new
      tree.insert(100)
      tree.insert('20.0')
      tree.insert(120)
      tree.insert(150.11)
      tree.insert(110.66)
      tree.insert('50.5')
      tree.insert(10)
      expect(tree.root.value).to equal 100.0
      expect(tree.root.right.value).to equal 120.0
      expect(tree.root.left.value).to equal 20.0
      expect(tree.root.right.right.value).to equal 150.11
      expect(tree.root.left.left.value).to equal 10.0
      expect(tree.root.right.left.value).to equal 110.66
      expect(tree.root.left.right.value).to equal 50.5
    end
  end
  describe '#correctly returns values as an array, sorted depending on the selected order' do
    it 'properly saves pre order in array' do
      tree = BinarySearchTree.new
      tree.insert(100)
      tree.insert('20.0')
      tree.insert(120)
      tree.insert(150.11)
      tree.insert(110.66)
      tree.insert('50.5')
      tree.insert(10.7)
      pre_order = []
      tree.pre_order(tree.root, pre_order)
      expect(pre_order).to eq [100.0, 20.0, 10.7, 50.5, 120.0, 110.66, 150.11]
    end
    it 'properly saves post order in array' do
      tree = BinarySearchTree.new
      tree.insert(100)
      tree.insert('20.0')
      tree.insert(120)
      tree.insert(150.11)
      tree.insert(110.66)
      tree.insert('50.5')
      tree.insert(10.7)
      post_order = []
      tree.post_order(tree.root, post_order)
      expect(post_order).to eq [10.7, 50.5, 20.0, 110.66, 150.11, 120.0, 100.0]
    end
    it 'properly saves in order in array' do
      tree = BinarySearchTree.new
      tree.insert(100)
      tree.insert('20.0')
      tree.insert(120)
      tree.insert(150.11)
      tree.insert(110.66)
      tree.insert('50.5')
      tree.insert(10.7)
      in_order = []
      tree.in_order(tree.root, in_order)
      expect(in_order).to eq [10.7, 20.0, 50.5, 100.0, 110.66, 120.0, 150.11]
    end
  end
end
