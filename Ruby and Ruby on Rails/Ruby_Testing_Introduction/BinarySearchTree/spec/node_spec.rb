#Jan Bienias
#238201

require 'node'

describe 'Node' do
  describe '#raises errors if arguments are invalid' do
    it 'raises an ArgumentError, when initialized with alphabetic string' do
      expect { Node.new('STRING') }.to raise_error(ArgumentError)
    end

    it 'raises an ArgumentError, when initialized partially correct string' do
      expect { Node.new('22ST89G12') }.to raise_error(ArgumentError)
    end
  end
  describe '#correct initializing' do
    it 'initializes correctly, when argument is integer' do
      n = Node.new(5)
      expect(n.value).to equal 5.0
      expect(n.left).to equal nil
      expect(n.right).to equal nil
    end

    it 'initializes correctly, when argument is floating type' do
      n = Node.new(21.37)
      expect(n.value).to equal 21.37
      expect(n.left).to equal nil
      expect(n.right).to equal nil
    end

    it 'initializes correctly, when argument is numeric string' do
      n = Node.new('21.37')
      expect(n.value).to equal 21.37
      expect(n.left).to equal nil
      expect(n.right).to equal nil
    end
  end
end
