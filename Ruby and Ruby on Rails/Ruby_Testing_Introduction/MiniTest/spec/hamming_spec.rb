require 'hamming'

describe '#compute' do
  it 'raises an error, when both strings have different length' do
    expect { Hamming.compute('AT', 'ATAT') }.to raise_error(ArgumentError)
    #expect(Hamming.compute('AT', 'ATAT')).to raise_error(ArgumentError)
    #does not work ^
  end

  it 'returns 0 for empty strings' do
    expect(Hamming.compute('', '')).to eq 0
  end

  it 'returns 0 for identical characters' do
    expect(Hamming.compute('X', 'X')).to eq 0
  end

  it 'returns 0 for identical strings' do
    expect(Hamming.compute('zosiasamosia', 'zosiasamosia')).to eq 0
  end

  it 'returns 1 for one difference between strings' do
    expect(Hamming.compute('ATAA', 'ATAT')).to eq 1
  end

  it 'returns 2 for two differences between strings' do
    expect(Hamming.compute('AAAAA', 'BAAAB')).to eq 2
  end
end
